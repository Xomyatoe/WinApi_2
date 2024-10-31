#include <Windows.h>
#include <cstdio>

#define IDC_STATIC 1000 //1) Создаем ResourceID для дочернего элемента
#define IDC_EDIT 1001
#define IDC_BUTTON 1002

CONST CHAR g_sz_WINDOW_CLASS[] = "Main Window";//Имя класса окна
CONST CHAR Information[] = "Main Window Размер: %ix%i, Позиция: ось X: %i ось Y: %i";

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
 
INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{
	//1) Регистрация класса окна
	// 1.1 создать и проинициализировать  структуру WNDCLASSEX
	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(wClass));

	wClass.style = NULL;
	wClass.cbSize = sizeof(wClass); //cb-count bytes
	wClass.cbClsExtra = 0;//Class extra bytes
	wClass.cbWndExtra = 0;// window extra bytes

	//Appearance:
	//wClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	//wClass.hIconSm = LoadIcon(NULL, IDI_APPLICATION); //Small icon
	wClass.hIcon = (HICON)LoadImage(hInstance, "ICO\\Lamp.ico", IMAGE_ICON, LR_DEFAULTSIZE,LR_DEFAULTSIZE,LR_LOADFROMFILE);
	wClass.hIconSm = (HICON)LoadImage(hInstance, "ICO\\Proc.ico", IMAGE_ICON, LR_DEFAULTSIZE,LR_DEFAULTSIZE,LR_LOADFROMFILE);

	//wClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	wClass.hCursor = (HCURSOR)LoadImage
	(
		hInstance,
		"CUR\\starcraft improved - default.ani",
		IMAGE_CURSOR,
		LR_DEFAULTSIZE,
		LR_DEFAULTSIZE,
		LR_LOADFROMFILE

	);
	wClass.hbrBackground = (HBRUSH)COLOR_WINDOW;

	wClass.hInstance = hInstance;
	wClass.lpfnWndProc = (WNDPROC)WndProc;
	wClass.lpszClassName = g_sz_WINDOW_CLASS;
	wClass.lpszMenuName = NULL;
	// 1.2 Вызвать функцию RegisterClassEx(...):
	if (RegisterClassEx(&wClass) == NULL)
	{
		MessageBox(NULL, "Class registration failed", "Error", MB_OK | MB_ICONERROR);
		return 0;
	}

	//2) Создание окна
	HWND hwnd = CreateWindowEx
	(
		NULL,
		g_sz_WINDOW_CLASS,//window class name
		g_sz_WINDOW_CLASS,//window title
		WS_OVERLAPPEDWINDOW,//window style
		CW_USEDEFAULT, CW_USEDEFAULT,//coordinates
		CW_USEDEFAULT, CW_USEDEFAULT,//window size
		NULL,//Parent
		NULL,//hMenu: для главного окна это ResourceID главного меню, 
		//для дочернего окна(кнопки и другое элементы) - это ID ресурса соответствующего элемента(кнопки, такстового поля и тд.)
		//По этому ResourceID мы находим дочерний элемент окна при помощи функции GetDlgItem()
		hInstance,
		NULL
	);
	if (hwnd == NULL)
	{
		MessageBox(NULL, "Window creation failed", "Error", MB_OK | MB_ICONERROR);
		return 0;
	}
	ShowWindow(hwnd, nCmdShow);//Задает режим отображения окна(Развернуто на весь экран,свернуто в окно, свернуто в панель задач)
	UpdateWindow(hwnd);//Прорисовывает окно
	//3) Запуск цикла сообщений
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0) > 0)
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.message;
	//Любое окно в операционной системе виндоус принадлежит какому то классу.
	//Существуют системные классы окон и прикладные(принадлежащие определенному приложению)
	// в свою очередь прикладные классы делятся на локальные и глобальные
	// к системным классам можно отнести следующие: button, listbox,combobox,edit, static, scrollbar,MDIclient
	// абсолютно к любому классу окна обращаются по его имени. Имя класса окна представляет собой строковую константу
    // Для регистрации прикладного класса окна нужно создать и проинициализировать  структуру
}
INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_CREATE:
		CreateWindowEx
		(
			NULL,
			"Static",
			"Это static_text, детка",
			WS_CHILD|WS_VISIBLE, //Для всех дочерних окон стиль обязательно будет WS_CHILD(это как минимум) и WS_VISIBLE
			100, 10,
			200, 20,
			hwnd,
			(HMENU)IDC_STATIC,
			GetModuleHandle(NULL),
			NULL
		);
		CreateWindowEx
		(
			NULL, "EDIT", "", WS_CHILD | WS_VISIBLE|WS_BORDER,
			100, 30,
			200, 20,
			hwnd,
			(HMENU)IDC_EDIT,
			GetModuleHandle(NULL),
			NULL
		);
		CreateWindowEx(
			NULL, "Button", "Применить",
			WS_CHILD | WS_VISIBLE,
			200, 55,
			100, 32,
			hwnd,
			(HMENU)IDC_BUTTON,
			GetModuleHandle(NULL),
			NULL
		);		
		break;
	case WM_MOVE:
	{
		CHAR windowsMoving[256]{}; 
		RECT rect; 
		GetWindowRect(hwnd, &rect); 
		int x = rect.right - rect.left;; 
		int y = rect.bottom - rect.top; 
		sprintf_s(windowsMoving, Information, x, y, rect.left, rect.top); 
		SetWindowText(hwnd, windowsMoving); 
		break;
	}
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON:
		{
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{};
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT);
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);

			HWND hStatic = GetDlgItem(hwnd, IDC_STATIC);
			SendMessage(hStatic, WM_SETTEXT, 0, (LPARAM)sz_buffer);
			SendMessage(hwnd, WM_SETTEXT, 0, (LPARAM)sz_buffer);
		}
			break;
		}
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
	case WM_CLOSE:
		DestroyWindow(hwnd); break;
	default: return DefWindowProc(hwnd, uMsg, wParam, lParam);
	}
	return FALSE;
}