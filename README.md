# NetBank System

## Team
Shuo Wang
Ming-jin Yeh

## Info
This is a web application based on ASP.net core as a prototype for Internet Banking Website in customer layer, which connect to the database in AZure by MVC model. 
The website is designed by EF and the data annotation and database constriants are satisfied a serious of standard.
For the function of Admin account, admin has a higher priority to operate customers' account, billpay and see the data analysis in different search filter, which shown in different charts.
![login](/web_login.png)

### It includes the following functions for customers:
#### My Profile:
Login/logout
change the password for the user
The session is 1 min

#### ATM: 
Deposit
Withdraw
Transfer money between accounts

#### My Statement:
check transaction history for different accounts

#### Bill Pays:
create bill pay event
edit bill pay event
delete bill pay event
check detaills bill pay event
According to you setting time to pay money in different payee periodically (annually, quarterly, monthly and once time )

### It includes the following functions for Admin, which get data directly from Web API:
#### Transactions: 
based on different filters to analyze transactions
depend on results to generate different kinds of graphs

#### User profile:
edit/delete/check user account details

#### Accounts:
lock & unlock user account

#### BillPay:
check the bill pay list for all uses
block & unblock user schedule payments


