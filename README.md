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


## How it Works?????
Initially a list of scripts from legitimate and illegitimate sites is saved in an xml file called the Whitelist and Blacklist respectively. When the user enters the URL the scripts from source of the webpage are extracted. The extracted scripts are saved in a xml file which maintains the greylist. Hash values for these scripts are calculated and compared with the hash values of the scripts in the Whitelist and Blacklist to determine whether the site to be accessed is free from xss attack or not.

#### Data Extraction Process:
The Source of the webpage obtained from the URL entered by the user is taken. Scripts from the Webpage Source are to be extracted. Extracted scripts are stored in the Greylist.

#### Usage of Hashing for more scripting!
Hash value for all the scripts extracted and stored in the greylist , and also for the scripts initial saved in the Whitelist and blacklist is calculated. The calculated hash values are used in comparing the scripts.

##### Secure Hash Algorithm (SHA512):
SHA512 is the hashing technique in which any arbitrary size message can be converted into fixed size of 512 bits. It processes the message as 1024 blocks including 128 bits for length at the last block. The blocks of messages are converted into fixed size message through compression functions which is either custom or block cipher based. 

##### SHA512 Implementation in Visual Basic:
*Namespace used:*  System.Security.Cryptography

*Methods used:* SHA1CryptoServiceProvider().ComputeHash(tmpSource) - Computes the hash value for the specified byte array.


#### Data Comparison:
Compares the hash value of scripts from greylist with that of Whitelist and blacklist. If the hash values match then accordingly the match count of blacklist or Whitelist is incremented.

### Actually what I did!
User enters the URL in the browser and then the Client Side XSS Guard algorithm extracts the scripts from the source of the webpage directed by the user entered URL. The extracted scripts are saved temporarily in a XML file called the Greylist. Hash values of scripts in the Greylist are compared with hash values of scripts in the Whitelist and the blacklist created initially, when the hash value matches the corresponding Whitelist or blacklist match count is incremented. If the Whitelist match count is greater than the blacklist match count then the entered URL directs to legitimate site so the site opens and if the blacklist match count is greater than the Whitelist match count then the entered URL directs to illegitimate site about which the user is warned.

### XSS GUARD ALGORITHM:
The following terminologies are used in the algorithm,
* WL: Whitelist
* BL: Blacklist
* GL: Greylist

![XSS Algorithm Flow](https://cloud.githubusercontent.com/assets/5390252/6542083/0562e1ba-c514-11e4-8cf6-58b1ee286b61.jpg)

The XSS guard algorithm works as follows. Initially the WhiteList, BlackList and GreyList are set to empty. Then manually some known scripts of both white and black type are stored in the database. The algorithm refers to all the webpages based on the scripts they contain, it checks the scripts with the whitelisted and blacklisted scripts and it judges the website on the basis of number of scripts matches with white and black list sites. If the numbers of scripts matched with WhiteList are more, then we add that site to the WhiteList. If the numbers of scripts matched with BlackList are more, then we add that site to the BlackList. If there are equal numbers of scripts matched with both white and black list, then we add that site to the new list named GreyList, which is left un-judged. After a few entries of new websites, the GreyList will be examined manually. And based on the result we list the website accordingly.

Initially, when the websites are checked the algorithm recognizes the scripts in them and lists them on the basis of XSS guard algorithm. Every time we visit the webpage the XSS algorithm runs and checks whether any new scripts are added to that webpage or not. If the number of scripts remains same, then there will be no change in the listing we made. If there are additional scripts in the webpage, then we can consult the owner of that site for the information regarding on the additional scripts. If the owner did not add any scripts, then we can consider that the webpage is hacked by someone and we have to take appropriate action according to the XSS guard algorithm.
