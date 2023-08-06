# _Dr. Sillystringz Factory App_

#### By _Austin Papritz_

#### _This web application completely organizes and calculates which engineers are licensed to work on which machine at Dr. Sillystringz Factory!_

## Technologies Used

* _C#_
* _ASP.NET Core_
* _JavaScript_
* _HTML/CSS_
* _MySQL_
* _Visual Studio Code_
* _Entity Framework Core_
* _JQuery_

## Description

_Dr. Sillystringz Factory App is a web app that has a detail page for an engineer, listing every machine an engineer is licensed to repair. Likewise, it has a machine page for every machine, listing every engineer that fulfills all the licenses to work on that machine. Navigate easily to create a new engineer, machine, or license. Easily update an engineer's license list or a machine's license requirements. The app automatically calculates whether an engineer meets the requirements to work on a machine._

## Project Setup

* _`Download ZIP` by clicking on the big green `Code` button._
* _Extract the ZIP to a designated location._
* _Open the `Factory.Solution` folder in your favorite code editor (e.g., VS Code, Xcode, Atom)._
* _Open the terminal, navigate to the project folder by entering `$ cd .\Factory\`_

## Database Setup

* _Search online to install MySQL on your computer. Remember your username and password._
* _Add `appsettings.json` file to project folder. Paste the following code, inserting your own information where {indicated}._

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database={DATABASENAME};uid={USERNAME};pwd={PASSWORD};"
    }
}
```

* _Build project by entering `$ dotnet build`._
* _Initialize the database by entering `$ dotnet EF migrations add InitialSetup`._
* _Complete database setup by entering `$ dotnet EF database update`._

## Run Web App

* _Enter `$ dotnet watch run` to run the web app._
* _Open your browser and enter `https://localhost:5012/` into the url bar, if it doesn't automatically._
* _You may need to give yourself security certs by entering `$ dotnet dev-certs https --trust`._
* _There will be a confirmation pop-up in your browser, you might also need to click `Advanced` and then click to proceed to site_
* _Enjoy!_

## Known Bugs

* _none_

## License

_This app is not licensed and is free to use and distribute._
_If you run in to any problems or have any suggestions/improvements, feel free to contact me on [linkedIn](https://www.linkedin.com/in/austin-papritz)!_
