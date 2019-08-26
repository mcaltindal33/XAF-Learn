;#DropDB WKLearnEasyTest

#Application WKLearn.Win
#Application WKLearn.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef WKLearn.Win

*Action About...
 
*Action OK

#EndIf

#IfDef WKLearn.Web
*Action Navigation(My Details)
#EndIf
