#include <Windows.h>
#include "resource.h"

CONST CHAR g_sz_LOGIN_INVITATION[] = "Введите имя пользователя";

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{
	//hinstance - экземпляр запущенного .exe файла нашей программы
	//hPrevInst - неиспользуется
	//LPSTR - LongPointer to String
	//lpCmdLine - commandline командная строка с параметрами запуска приложения
	//nCmdShow - режим отображения окна(развернуто на весь экран, свернуто в панель задач)
	// Префиксы n..., lp... это Венгерская нотация
	// n- Number
	// lp- Long Pointer
	//!!!!!!!!!!!Модальным называется окно которое блокирует родительское окно!!!!!!!!!!
	//У каждого окна есть процедура

	/*MessageBox(
		NULL,
		"Hello Windows!\n This is MessageBox()",
		"Window title",
		MB_CANCELTRYCONTINUE | MB_HELP| MB_DEFBUTTON3 |
		MB_ICONINFORMATION  |
		//MB_SYSTEMMODAL 
		//MB_DEFAULT_DESKTOP_ONLY |
		//MB_RIGHT 
		//MB_TOPMOST
		//MB_SERVICE_NOTIFICATION
		
	);*/

	DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, DlgProc, 0);

	return 0;
}
BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	//hwnd - Handler to window обработчик или дескриптор окна- это число при помощи которого можно обратиться к окну
	//uMsg -  Message Сообщение которое отправляется  окну
	//wParam, lParam - это параметры сообщения, у кажждого сообщения свой набор параметров.
	//LOWORD - Младшее слово
	//HIWORD - старшее слово
	//DWORD -
	switch (uMsg)
	{
	case WM_INITDIALOG: //Это сообщение отправляется один раз при инициализации окна
	{

		HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_LOGIN);
		SendMessage(hEdit, WM_SETTEXT, 0,(LPARAM) g_sz_LOGIN_INVITATION);
	}
		break;
	case WM_COMMAND: // Обрабатывает нажатие кнопок и др. действия пользователя

		switch (LOWORD(wParam))
		{
		case IDC_EDIT_LOGIN: 
		{
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{};
			SendMessage((HWND)lParam, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);
			if (HIWORD(wParam) == EN_SETFOCUS && strcmp(sz_buffer, g_sz_LOGIN_INVITATION) == 0)
					SendMessage((HWND)lParam, WM_SETTEXT, 0, (LPARAM)"");
			
			if (HIWORD(wParam) == EN_KILLFOCUS && strcmp(sz_buffer, "") == 0)
					SendMessage((HWND)lParam, WM_SETTEXT, 0, (LPARAM)g_sz_LOGIN_INVITATION);
			// функция strcmp(const char* str1, const char* str2) сравнивает строки и возвращает значение типа int:
			// 0- строки идентичны
			// !0 - строки отличаются
			//EN-edit notification
		}
		//Когда элемент окна получает фокус, он отправляет своему родителю сообщение WM_COMMAND, wParam которого LOWORD вкладывает ID ресурса LOWORD(wParam)=ResourceID, которому
		//он привязан(элемент получивший фокус), а в HIWORD(wParam)=NotificationCode(EN_SETFOCUS)
			break;
		case IDC_BUTTON_COPY: 
		{
			//1) создаем буфер
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE] = {}; //sz_ - string zero (NULL terminated Line - C-string)
			//2) Получаем обработчики текстовых полей:
			HWND hEditLogin = GetDlgItem(hwnd, IDC_EDIT_LOGIN);
			HWND hEditPassword = GetDlgItem(hwnd, IDC_EDIT_PASSWORD);
			//3) считываем содержимое поля "Login" в буфер:
			SendMessage(hEditLogin, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);
			//4) Записываем полученный текст в тестовое поле "PASSWORD"
			SendMessage(hEditPassword, WM_SETTEXT, 0, (LPARAM)sz_buffer);
		}
			break;
		case IDOK: MessageBox(hwnd, "Была нажата кнопка ОК", "Info", MB_OK | MB_ICONINFORMATION); break;
			case IDCANCEL : EndDialog(hwnd, 0);  break;
	    }
		break;
	case WM_CLOSE: // Отправляется при нажатии кнопки "закрыть"
		EndDialog(hwnd, 0);
		break;
    }
	return FALSE;
}