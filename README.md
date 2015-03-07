# Client-Side-XSS-Guard
Cyber Security Project - for client side Cross Site Scripting (XSS) detection. It involves detection and Prevention of Cross Site Scripting (XSS) using “Client Side XSS Guard Algorithm” in a Browser based Environment.

##XSS
1. Injecting client-side malicious script into Web pages
2. One of the most prevalent growing threats
* Pave way for Phishing  attacks, Account hijacking, Cookie stealing, DOS, Web Content manipulation
* 73% of website – vulnerable to XSS – (Survey from Kaspersky Lab)
* Ranked 1in OWASP Top 10 Security Risks (2014)
* Detection in server side is hard
* Possible to detect only at Client side

Basically, this project presents a new solution to block Cross Site Scripting (XSS) attacks that is independent of the languages in which the web applications are developed and addresses XSS vulnerabilities arise from other interfaces.

The solution is modularized, configured, and developed in Visual Basic.Net, XML. It provides the flexibility to be used across languages with a very minimal configuration to prevent XSS.

Here users are allowed to browse the websites as they do in other commercial browsers for increasing the user’s flexibility. When the user input (website URL) is given, the browser loads the page, before this process it extracts the script elements in the loaded page and stores in a GreyList XML Database. After comparing the contents of the GreyList with existing BlackList and WhiteList Database, the browser decides whether to allow the page or to discard.

In this project, the users are provided with security mechanisms while accessing any web page. The vulnerabilities are detected by comparing the existing script database with the script elements generated from the web pages. If vulnerability is not found, then the actual output can be provided to the users, else the loading page will be blocked. Thus the users are prevented from redirection to the malicious web pages.

Client Side XSS-Guard works by discovering intentions of the web application, and uses the following criteria in order to prevent the attacks. It rests mainly on two simple observations:
* Web applications are written implicitly assuming benign inputs, and encode programmer intentions to achieve a certain HTML response on these inputs.
* Maliciously crafted inputs subvert the program into straying away from these intentions, leading to a HTML response that leads to XSS-attacks.

