/*
 * MathConverter and accompanying samples are copyright (c) 2011 by Ivan Krivyakov
 * ivan [at] ikriv.com
 * They are distributed under the Apache License http://www.apache.org/licenses/LICENSE-2.0.html
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// Value converter that performs arithmetic calculations over its argument(s)
    /// </summary>
    /// <remarks>
    /// MathConverter can act as a value converter, or as a multivalue converter (WPF only).
    /// It is also a markup extension (WPF only) which allows to avoid declaring resources,
    /// ConverterParameter must contain an arithmetic expression over converter arguments. Operations supported are +, -, * and /
    /// Single argument of a value converter may referred as x, a, or {0}
    /// Arguments of multi value converter may be referred as x,y,z,t (first-fourth argument), or a,b,c,d, or {0}, {1}, {2}, {3}, {4}, ...
    /// The converter supports arithmetic expressions of arbitrary complexity, including nested subexpressions
    /// </remarks>
    public class MathConverter :
#if !SILVERLIGHT
        MarkupExtension,
        IMultiValueConverter,
#endif
        IValueConverter
    {
        readonly Dictionary<string, IExpression> m_StoredExpressions = new Dictionary<string, IExpression>();

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(new[] { value }, targetType, parameter, culture);
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>将源值转换为绑定源的值。数据绑定引擎在将值从绑定源传播给绑定目标时，调用此方法。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。<see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 的返回值表示转换器没有生成任何值，且绑定将使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />（如果可用），否则将使用默认值。<see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> 的返回值表示绑定不传输值，或不使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> 或默认值。</returns>
        /// <param name="values">
        /// <see cref="T:System.Windows.Data.MultiBinding" /> 中源绑定生成的值的数组。值 <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 表示源绑定没有要提供以进行转换的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = Parse(parameter.ToString()).Eval(values);
                if (targetType == typeof(decimal))
                {
                    return result;
                }
                if (targetType == typeof(string))
                {
                    return result.ToString(CultureInfo.InvariantCulture);
                }
                if (targetType == typeof(int))
                {
                    return (int)result;
                }
                if (targetType == typeof(double))
                {
                    return (double)result;
                }
                if (targetType == typeof(long))
                {
                    return (long)result;
                }

                throw new ArgumentException(String.Format("Unsupported target type {0}", targetType.FullName));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>将绑定目标值转换为源绑定值。</summary>
        /// <returns>从目标值转换回源值的值的数组。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetTypes">要转换到的类型数组。数组长度指示为要返回的方法所建议的值的数量与类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

#if !SILVERLIGHT
        /// <summary>在派生类中实现时，返回一个对象，此对象被设置为此标记扩展的目标属性的值。</summary>
        /// <returns>要在应用了扩展的属性上设置的对象值。</returns>
        /// <param name="serviceProvider">可以为标记扩展提供服务的对象。</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void ProcessException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private IExpression Parse(string s)
        {
            IExpression result;
            if (!m_StoredExpressions.TryGetValue(s, out result))
            {
                result = new Parser().Parse(s);
                m_StoredExpressions[s] = result;
            }

            return result;
        }

        interface IExpression
        {
            decimal Eval(object[] args);
        }

        class Constant : IExpression
        {
            private readonly decimal m_Value;

            public Constant(string text)
            {
                if (!decimal.TryParse(text, out m_Value))
                {
                    throw new ArgumentException(String.Format("'{0}' is not a valid number", text));
                }
            }

            public decimal Eval(object[] args)
            {
                return m_Value;
            }
        }

        class Variable : IExpression
        {
            private readonly int m_Index;

            public Variable(string text)
            {
                if (!int.TryParse(text, out m_Index) || m_Index < 0)
                {
                    throw new ArgumentException(String.Format("'{0}' is not a valid parameter index", text));
                }
            }

            public Variable(int n)
            {
                m_Index = n;
            }

            public decimal Eval(object[] args)
            {
                if (m_Index >= args.Length)
                {
                    throw new ArgumentException(String.Format("MathConverter: parameter index {0} is out of range. {1} parameter(s) supplied", m_Index, args.Length));
                }

                return System.Convert.ToDecimal(args[m_Index]);
            }
        }

        class BinaryOperation : IExpression
        {
            private readonly Func<decimal, decimal, decimal> m_Operation;
            private readonly IExpression m_Left;
            private readonly IExpression m_Right;

            public BinaryOperation(char operation, IExpression left, IExpression right)
            {
                m_Left = left;
                m_Right = right;
                switch (operation)
                {
                    case '+':
                        m_Operation = (a, b) => ( a + b );
                        break;
                    case '-':
                        m_Operation = (a, b) => ( a - b );
                        break;
                    case '*':
                        m_Operation = (a, b) => ( a * b );
                        break;
                    case '/':
                        m_Operation = (a, b) => ( a / b );
                        break;
                    default:
                        throw new ArgumentException("Invalid operation " + operation);
                }
            }

            public decimal Eval(object[] args)
            {
                return m_Operation(m_Left.Eval(args), m_Right.Eval(args));
            }
        }

        class Negate : IExpression
        {
            private readonly IExpression m_Param;

            public Negate(IExpression param)
            {
                m_Param = param;
            }

            public decimal Eval(object[] args)
            {
                return -m_Param.Eval(args);
            }
        }

        class Parser
        {
            private string m_Text;
            private int m_Pos;

            public IExpression Parse(string text)
            {
                try
                {
                    m_Pos = 0;
                    m_Text = text;
                    var result = ParseExpression();
                    RequireEndOfText();
                    return result;
                }
                catch (Exception ex)
                {
                    var msg =
                        String.Format("MathConverter: error parsing expression '{0}'. {1} at position {2}", text, ex.Message, m_Pos);

                    throw new ArgumentException(msg, ex);
                }
            }

            private IExpression ParseExpression()
            {
                var left = ParseTerm();

                while (true)
                {
                    if (m_Pos >= m_Text.Length)
                    {
                        return left;
                    }

                    var c = m_Text[m_Pos];

                    if (c == '+' || c == '-')
                    {
                        ++m_Pos;
                        var right = ParseTerm();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseTerm()
            {
                var left = ParseFactor();

                while (true)
                {
                    if (m_Pos >= m_Text.Length)
                    {
                        return left;
                    }

                    var c = m_Text[m_Pos];

                    if (c == '*' || c == '/')
                    {
                        ++m_Pos;
                        var right = ParseFactor();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseFactor()
            {
                SkipWhiteSpace();
                if (m_Pos >= m_Text.Length)
                {
                    throw new ArgumentException("Unexpected end of text");
                }

                var c = m_Text[m_Pos];

                if (c == '+')
                {
                    ++m_Pos;
                    return ParseFactor();
                }

                if (c == '-')
                {
                    ++m_Pos;
                    return new Negate(ParseFactor());
                }

                if (c == 'x' || c == 'a')
                {
                    return CreateVariable(0);
                }
                if (c == 'y' || c == 'b')
                {
                    return CreateVariable(1);
                }
                if (c == 'z' || c == 'c')
                {
                    return CreateVariable(2);
                }
                if (c == 't' || c == 'd')
                {
                    return CreateVariable(3);
                }

                if (c == '(')
                {
                    ++m_Pos;
                    var expression = ParseExpression();
                    SkipWhiteSpace();
                    Require(')');
                    SkipWhiteSpace();
                    return expression;
                }

                if (c == '{')
                {
                    ++m_Pos;
                    var end = m_Text.IndexOf('}', m_Pos);
                    if (end < 0)
                    { --m_Pos; throw new ArgumentException("Unmatched '{'"); }
                    if (end == m_Pos)
                    { throw new ArgumentException("Missing parameter index after '{'"); }
                    var result = new Variable(m_Text.Substring(m_Pos, end - m_Pos).Trim());
                    m_Pos = end + 1;
                    SkipWhiteSpace();
                    return result;
                }

                const string decimalRegEx = @"(\d+\.?\d*|\d*\.?\d+)";
                var match = Regex.Match(m_Text.Substring(m_Pos), decimalRegEx);
                if (match.Success)
                {
                    m_Pos += match.Length;
                    SkipWhiteSpace();
                    return new Constant(match.Value);
                }
                else
                {
                    throw new ArgumentException(String.Format("Unexpeted character '{0}'", c));
                }
            }

            private IExpression CreateVariable(int n)
            {
                ++m_Pos;
                SkipWhiteSpace();
                return new Variable(n);
            }

            private void SkipWhiteSpace()
            {
                while (m_Pos < m_Text.Length && Char.IsWhiteSpace(( m_Text[m_Pos] )))
                {
                    ++m_Pos;
                }
            }

            private void Require(char c)
            {
                if (m_Pos >= m_Text.Length || m_Text[m_Pos] != c)
                {
                    throw new ArgumentException("Expected '" + c + "'");
                }

                ++m_Pos;
            }

            private void RequireEndOfText()
            {
                if (m_Pos != m_Text.Length)
                {
                    throw new ArgumentException("Unexpected character '" + m_Text[m_Pos] + "'");
                }
            }
        }
    }
}