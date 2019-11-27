# PersonalBlog
This product is made by **.NET CORE 3.0 Blazor Server-Side** for **Single User Only** whitch is the blog owner. It is a very simply blog.

# Technical Support
* .NET Core 3.0
* Blazor Server-Side
* Microsoft.EntityFrameworkCore 3.0
* Sqlite 4.3.x
* Bootstrap 4.3.x
* Visual Studio 2019 for Solution

### [Visit the Demo](https://vblog.chinacloudsites.cn/)

# Change Logs(v1.0)
* **1.0**
    - [add]Localization Support
    - [fix]The rest of Bugs
    - [optimize]Some page layouts
* **0.2**
    - [add]Category maintainance
    - [add]System maintainance
    - [add]Localization function, English only now
    - [update]all pages
* **0.1**
  - Create/Update the post
  - Configure your application


# Getting Start
### Using Solution by VS2019
Click `F5` to run the solution.

### Publish
Using the command bellow to publish application
> dotnet publish -o "your output path" -configuration release

and use the command to run this application after publish
> dotnet PersonalBlog.App.dll

### Modify `Data\settings.json` to configure your application
Open the `Data\settings.json` from the root path, you will see the json format like this:
```json
 {
    "Title": "PersnalBlog", //The title of blog
    "Description": "A personal blog by .NET CORE   Blazor.",//The description of blog
    "UserName": "admin", //username of admin
    "Password": "123456",//password of admin
    "AppSecret": "oi23intf0924hgfowirgw0293rf" // secret key for jwt authentication, do not update!!!
    "Culture":"en-us", support localization
  }
```
**Restart application after save changes**

