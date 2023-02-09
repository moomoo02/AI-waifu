# AI Waifu 
 Are you lonely?  Do you need a friend?  Because I DO.  Inspired by *chatgpt* and *vtubers*, I attempt to create a virtual FRIEND on the browser.  I'll also be learning **C#** and **.NET** for the first time!  Below are pictures of the progress I'm making *(I sure hope my employers dont see this...)*
 <table><tr><tr>
            <td valign="bottom">
            <img src="./memories/model.gif" width="200"><br>
            model.gif | Feb 04 2023
            </td>
            <td valign="bottom">
            <img src="./memories/CRUD.png" width="200"><br>
            CRUD.png | Feb 05 2023
            </td>
            <td valign="bottom">
            <img src="./memories/SimpleChat.png" width="200"><br>
            SimpleChat.png | Feb 06 2023
            </td>
            <td valign="bottom">
            <img src="./memories/Expressions.gif" width="200"><br>
            Expressions.gif | Feb 08 2023
            </td></tr><tr>
            <td valign="bottom">
            <img src="./memories/SentimentAnalysis.gif" width="200"><br>
            SentimentAnalysis.gif | Feb 09 2023
            </td></tr></table>

# Journal

## Feb 4
* Loaded in model.  SHE IS SO PRETTYY!! I'm still in disbelief how advanced technology as come...

## Feb 5
* Created backend in Node.js, but then I realized I wanted to try .NET, so I converted to .NET.  
* Watched a 3 hour long tutorial to set up a simple backend and MongoDB database.
* Installed Postman to test out the Api

## Feb 6
* Added a simple chat ui using some chat ui package.  Spent about 3 hours just trying to align the canvas and chat side by side (My css skills suck)
* Added a new text to text controller in the backend that sends a new message DTO.
  
## Feb 7
* Wrote a really long prompt about the waifu, containing personality traits, lore, family, hobbies, occupation, and flaws.
* Injected openai api dependency into text to text controller.  Using above prompt to create completions and then storing the content within a message DTO to send.
* Asked her to play league with me and she said no T-T

## Feb 8
* Didn't get to work on it much today since I was out.  I did look into how to manipulate the model and found 8 different expressions and 1 motion that I can work with.  Haven't figured out how to move her mouth yet.

## Feb 9
* Wrote a new prompt for the openai chat model on classifying emotions of a text message.  Now, in addition to the message, the Text-Text endpoint will also return an emotion associated with the message.  SHE NOW HAS EXPRESSIONS ADN EMOTIONS!!!!K WAIUDJNAWKDNKAWJNDWK