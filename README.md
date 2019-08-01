<div align="center">

# <a href="https://pcdmz.com/" target="_blank" rel="noopener noreferrer">Sleek (ASP.NET Core)</a>

</div>

<div align="center">

[![GitHub stars](https://img.shields.io/github/stars/pcdmz/one.svg?color="brightgreen"&style=flat-square)](https://github.com/pcdmz/sleek/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/pcdmz/one.svg?color="success"&style=flat-square)](https://github.com/pcdmz/sleek/network)
[![GitHub issues closed](https://img.shields.io/github/issues-closed-raw/pcdmz/sleek.svg?color="orange"&style=flat-square)]() 
<a href="https://coveralls.io/github/chartjs/Chart.js?branch=master"><img src="https://img.shields.io/coveralls/chartjs/Chart.js.svg?&style=flat-square&maxAge=600" alt="Coverage"></a>
<a href="https://github.com/chartjs/awesome"><img src="https://awesome.re/badge-flat2.svg?&style=flat-square" alt="Awesome"></a>
![license](https://img.shields.io/badge/license-MIT-blue.svg?&style=flat-square)

</div>

<div align="center">

![Product Gif](/admin/assets/images/sleek.gif)

</div>

# Sleek
This is a fully functional version of the MIT License Sleek Dashboard. Developed with Visual Studio 2017 and ASP.NET Core 2, it implements every feature provided by the template.

##Latest Release
Users can sign in and controls are kept in context with their credentials. (Admin, Manager or Ordinary User)

## Important

This solution is a new work in progress and will be getting updates for several more days. Feel free to look it over, but many parts of it are still incomplete.

## Known Issues

* Registration and Password Recovery are unfinished and first on the list for completion.
* The system is based on a Customer with multiple users. Customers are created during the registration process and can create an unlimited number of users for their account.
* Currently, there are no custom validation attributes. Several will be added as the project progresses. (Model validation)
* The project is compatible with ASP.NET Core identity, but does not use the built-in tables for role management.

## Overview

If you are developing and Enterprise scale application or working with a team of developers, I would suggest you start with one of the excellent [Free and Commercial Templates](https://aspnetboilerplate.com/Templates) available from [ASP.NET Boilerplate](https://aspnetboilerplate.com) as they are available in Angular, Vue and React versions and have been built specifically for this purpose.

## Current Features

* Developed with ASP.NET Core 2.2.1 and Microsoft SQL Server 2016 (Windows or Linux)
* Maintains compatibility with multiple database vendors by avoiding stored procedures and triggers (EF6 / LINQ)
* Supports claims-based authentication without the complexity of the built-in user tables

## Coming Soon

In addition to the functionality already implemented, the following features will soon be available:

* Complete documentation with detailed instructions.
* Built-in project management system with support for GIT and Mercurial.
* Entity Framework Based API with JWT Authentication
* Mailing address correction and certification via the USPS Webtools API
* Functioning Notification, Message and Alert Centers
* Microsoft SignalR-Based Chat
* Website Search Feature

## Change History

* Sign-up, Password Recovery and Contact forms set the form autocomplete="off" and the field autocomplete="new-password".

### Prerequisites

The project was built using Visual Studio Community 2017, ASP.NET Core 2.2.1 and SQL Server 2016. Make sure you have all three installed before loading the project for the first time.

## Getting Started

All you need do is clone or unzip the project to a local folder and then open it in Visual Studio. Launch the Nuget Package Management Console, run the Database Migration script and start coding.

## Built With

* [Visual Studio Community](https://visualstudio.microsoft.com/) - Integrated Development Environment
* [ASP.NET Core 2.2.1](https://docs.microsoft.com/en-us/aspnet/?view=aspnetcore-2.2) - .NET Core Framework
* [Sleek Admin Dashboard Template](https://github.com/tafcoder/sleek-dashboard) - Bootstrap 4 Admin Template
* [Bootstrap 4](https://getbootstrap.com/) - Component Library
* [Chart.js](https://www.chartjs.org/) - Javascript Charting
* [jQuery](https://jquery.com/) - JavaScript Library
* [Fontawesome Free](https://rometools.github.io/rome/) - CSS Icon Library
* [Ionicons Free](https://github.com/ionic-team/ionicons) - SVG and Web Font Icon Library

## Browser Support

At present, we officially try to support the following browsers:

<img src="/one/one/admin/assets/img/github/chrome.png" width="64" height="64"> <img src="/one/one/admin/assets/img/github/firefox.png" width="64" height="64"> <img src="/one/one/admin/assets/img/github/edge.png" width="64" height="64"> <img src="/one/one/admin/assets/img/github/safari.png" width="64" height="64"> <img src="/one/one/admin/assets/img/github/opera.png" width="64" height="64">

## Reporting Issues

We use GitHub Issues as the official bug tracker for **Sleek**. Please Search [existing issues](https://github.com/pcdmz/sleek/issues). It’s possible someone has already reported the same issue.
If your problem or idea is not addressed yet, [open a new issue](https://github.com/pcdmz/sleek/issues)

## Technical Support or Questions

If you have questions or need help integrating this project please [contact us](mailto:admin@pcdmz.com) instead of opening an issue.

## Licensing

- Copyright 2019 Karl Williams (https://pcdmz.com/)
- Licensed under MIT (https://github.com/pcdmz/sleek/blob/master/LICENSE.md)
