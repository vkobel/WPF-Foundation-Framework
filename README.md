WPF-Foundation-Framework
========================

> First of all this is a **work in progress**

This project provide a quick and easy-to-use WPF framework separated into two sub-frameworks:
**FoundationData** and **FoundationWPF**

Main Features
-------------

+ The Data framework implements a generic `Repository<TEntity>` that provide standard methods to access and persist POCO form the Entity Framework				      
+ Two levels of navigation (with the possiblilty to implement more)
	+ Described by the `NavigAttribute` 
	+ Uses dependency injection via **Ninject**
+ Instant and automatic ViewModel creation and `ViewModelCollection` right from the `Repository<TEntity>`
+ Possibility to have the ViewModels implementing `IPreLoadable` to enable asynchronous loading
+ Each ViewModel is represented by a `ViewModelProxy<TEntity>` which exposes a `BindingData` property to bind to
	+ `BindingData` contains all the entity properties as well as the new ones defined in the ViewModel class that inherits from `ViewModelProxy<TEntity>`
+ The MVVM Framework used here is **MVVM Light Toolkit**

Samples
-------

Samples of code will eventually come one day ;)

