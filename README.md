# Aurum

####Datalayer management for complete control freaks 

Aurum is an ORM alternative that automates the creation and maintenance of code across multiple layers of the application. 
Code and query generation takes place at design time leaving clean, efficient, easy to debug code - Aurum introduces no runtime dependencies.


The central objective of Aurum is to automate as much as possible without eliminating any control over the final code. 
Unlike many code generation tools Aurum allows interactive editing of both templates and generated code. 
Changes are merged intelligently to allow updates to the template to be propagated without losing modifications to generated classes. 

Finally Aurum provides advanced tools to manage queries and check for errors or inefficiencies like unused or unmapped columns. 
The application can be installed as a stand-alone WPF application or as a VisualStudio extension. 

---
This project is currently in development

|Branch      |Build Status          |
| ------------- |:-------------:|
|Development    |[![Build status](https://ci.appveyor.com/api/projects/status/hk54xtaqc8q62c3m/branch/Development?svg=true)](https://ci.appveyor.com/project/404htm/aurum/branch/Development)|
|Master         | |

---

####What distinguishes aurum from traditional orms?
With traditional ORMs you trade some performance and a lot of control for convenience. This isn't necessarily a bad thing; Developer time is valuable and repetitive mapping code is both error-prone and deeply soul-crushing. The problem is that a lot of this power provided by traditional ORMs can't even be leveraged in most structured applications. 

Generally professional-grade software has a discrete data-layer. The ability to walk arbitrary object trees in such a scenario is useless. You retrieve data from a specific method for a specific purpose. Lazy loading seems useful at first but ends up burning new devs enough that it can practically be considered as an ORM rite of passage. Frameworks like EF also seem to treat attached entities as the standard case - In most real-world applications this isn't an option. Objects are passed across web-services, used for short lived requests, or used across many classes in a way that makes keeping track of a context unwieldy. Objects can be detached, and reattached but the syntax is frequently awkward and full of pitfalls. 

As a developer your time should be spent solving problems, not fighting the abstraction you are building on. To this end a less powerful abstraction that is better aligned with real-world applications is preferable. 


---


