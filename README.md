### SKQuotes
群益自動收集Tick程式 Automatic Quote Machine
此程式並非用來看盤, 主要用於載tick時做盤中的策略下單

群益 API 官網
<https://www.capital.com.tw/Service2/download/API.asp>

### Working Enviroment 
* Windows 10 Professional (64-bit)</br>
* Microsoft SQL Server 2016 Developer (64-bit)
* 群益API 版本2.13.16, 必要環境輔助安裝工具 (Visual C++ 可轉發套件)

### 提供功能:
1. 程式啟動後會自動做登入動作, 無須操作即可將Tick load到database
2. 自動載入前日分K, 日K資料. 判斷僅用weekday-1, 並無特別使用交易日
3. 使用exe.Config檔參數載入
4. 在sql agent裡建一個job執行ChkTick, 確認tick使否持續運作, 如果沒有的話就執行ps重啟SKQoute

### 使用方法
1. 安裝SKCOM裡的dll, 右鍵執行install.bat
2. 將query裡所需要用到的table, function procedure 建好
3. 將powershell stopprocess.ps放在適當位置, 如SQL agent偵測到沒有新tick時會透過這個ps重啟程式
4. 新建一個SQL agent job跑 <b>ChkTick</b> at 8:45
5. 填入exe.config裡的參數
6. 將程式設定在工作排程裡定時啟用

### 注意事項
* 程式並不會備份歷史tick, 需自行建job備份
* 程式會與報價server斷線如超過一段時間沒有new tick
* 群益API版本必須一致, 否則程式可能開不起來
* 必須有群益期貨帳號, 並且開通 API 使用權限後才能使用(通常為申請API隔日生效)


