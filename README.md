# MinimalOwinWebApiSelfHost

This repo contains source code for a series of articles. Each article builds upon the work done in the last. In order that the source for each article make sense in the context of the article, I have separated branches relevant to each article. 

* **Branch: Master -** Will always contain all of the changes, and be up to date with the most recent article in the series

The branches below are both referred to in [ASP.NET Web Api 2.2: Create a Self-Hosted OWIN-Based Web Api from Scratch](http://typecastexception.com/post/2015/01/11/ASPNET-Web-Api-22-Create-a-Self-Hosted-OWIN-Based-Web-Api-from-Scratch.aspx)

* **Branch: api -** The minimal example implementation of a self-hosted Web Api application using OWIN/Katana
* **Branch: ef -** Extends the example above by adding entity framework and a local, file-based database (SQL CE)

The next branch, `auth-minimal` is referred to in the post [ASP.NET Web Api: Understanding OWIN/Katana Authentication/Authorization Part I: Concepts](http://typecastexception.com/post/2015/01/19/ASPNET-Web-Api-Understanding-OWINKatana-AuthenticationAuthorization-Part-I-Concepts.aspx)

* **Branch: auth-minimal -** Adds simplified authentication examples to the previous branches. Identity is not used, and database persistence is not yet implemented for the auth stuff. 

The next branch, auth-db is referred to in the post [ASP.NET Web Api: Understanding OWIN/Katana Authentication/Authorization Part II: Models and Persistence](http://typecastexception.com/post/2015/01/25/ASPNET-Web-Api-Understanding-OWINKatana-AuthenticationAuthorization-Part-II-Models-and-Persistence.aspx)

* **Branch: auth-db -** Adds entity models and database persistence.
* 

The Final Branch, `auth-identity` is referred to in the post [ASP.NET Web API: Understanding OWIN/Katana Authentication/Authorization Part III: Adding Identity](http://typecastexception.com/post/2015/02/15/ASPNET-Web-API-Understanding-OWINKatana-AuthenticationAuthorization-Part-III-Adding-Identity.aspx)

* **Branch: auth-identity ** Replace bare-bones roll-our-own implementation with a minimal Identity framework implementation.


