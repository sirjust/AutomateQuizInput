This program automates the process of uploading quizzes using the admin portal of anytimece.com

It requires two perfectly formatted documents: Quizzes.txt and PageInfo.txt
NOTE: Github has messed with the formatting of this document, so please open it in a text editor for the correct formatting
An example of the formatting of Quizzes.txt is as follows:

**IMPORTANT**
DO NOT READ THIS IN GITHUB! GITHUB REFORMATS THIS DOCUMENT WITH MARKDOWN THAT WILL BREAK THE PROGRAM IF FOLLOWED
**IMPORTANT**

Quiz 1
1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.
42
54
67
80*

2) Size the water heater for a house with 1 Bathroom and 3 Bedrooms.
42
54*
67
80

Quiz 2
1) Question 1, false is correct
True
False*

2) Question 2, true is correct
True*
False

The asterisk (*) indicates the correct answer. It must be included once and only once for each set of answers

PageInfo.txt is formatted as follows:

Quiz 1
20
21
2

Quiz 2
50
51
21

Quiz 3
100
101
51

The first number following each quiz refers to the page the quiz is found on, the second number is where the user
is redirected after successful completion. The final number is the page the user is sent to upon failure.

IMPORTANT: the admin portal requires there be no single quotes (') anywhere in the text. Please conduct a search
(using ctrl-f) of the single quote and verify the text files are clean of them before running the program.

IMPORTANT: There must be a blank line after every question, including the final question in the document. If it is not
included, that question will not be input.