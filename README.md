# SKQuotes
群益自動收集Tick程式 Automatic Quote Machine

# Working Enviroment 
Windows 10 Professional (64-bit)</br>
Microsoft SQL Server Developer (64-bit)

# Purpose:
Save the the latest tick in the table and run against strategy. Note, this program clear the tick table every time it runs,
it does not save history data

This program will not stay connected to the Capital Solace Quote Server for too long, if no new ticks is receving. 

# Query included
1. table schema StockHisotryMin
2. table schema StockHistoryDaily
3. table schema TickData
4. table value function GetTodayTickAM
5. store procedure sp_GetTicksIn5Min
6. store procedure sp_GetTicksDaily
7. store procedure ChkTick


# How to use
1. Use the Query.sql to create table, sp, and function needed for later use
2. Put the stopprocess.ps at desired path
3. Create a SQL agent job run <b>ChkTick</b> at 8:45
4. Fill in the login credential

