// CppCaller.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include <atlbase.h>
#include <atlcom.h>
#include <iostream>

#import "..\..\..\..\..\Communication\Yunda.COM.MMICommunication.tlb" named_guids raw_interfaces_only

//_COM_SMARTPTR_TYPEDEF(ComTester::IComTest, __uuidof(ComTester::IComTest));

ATL::CComPtr<Yunda_COM_MMICommunication::ICommunication> pt;



void OnRecv(byte* bools, int blength, float* floats, int flength)
{
	std::cout << "c++ call back" << std::endl;

	SAFEARRAY* pb; 
	pb = SafeArrayCreateVector(VT_UI1, 0, blength);
	pb->pvData = bools;

	SAFEARRAY* pf;
	pf = SafeArrayCreateVector(VT_R4, 0, flength);
	pf->pvData = static_cast<float*>(floats);

	auto h1 = pt->SendAsyn(pb, pf);
	if (FAILED(h1))
	{

	}
	SafeArrayDestroy(pb);
	SafeArrayDestroy(pf);
}

int main()
{
	CoInitialize(NULL);

	ATL::CComPtr<Yunda_COM_MMICommunication::ICommunication> pc;

	auto hr = pt.CoCreateInstance(__uuidof(Yunda_COM_MMICommunication::Communication));
	if (FAILED(hr))
	{

	}
	auto hs = pt->SetRecvHandler(reinterpret_cast<long>(&OnRecv));
	if (FAILED(hs))
	{

	}

	auto h1 = pt->Initalize();	
	if (FAILED(h1))
	{

	}

	char c;
	std::cin >> c;

	CoUninitialize();

	return 0;
}

