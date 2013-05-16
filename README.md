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

#### Simplest possible ViewModel

All ViewModelProxy<> exposes the all proxied properties through `BindingData`.

```cs
class PersonViewModel : ViewModelProxy<Person> {
   public PersonViewModel(Person p) : base(p) {
   }
}
```

#### A Collection ViewModel to display a list of person and their details, with navigation `Humans -> Person`

Every ViewModelCollection exposes a public member `CollectionView`.

```cs
[Navig("Humans", "Person")]
class PersonCollectionViewModel : ViewModelCollection<Person, PersonViewModel> {
}
```

#### Usage in the view

Access a simple property:
```
<(...) Text="{Binding BindingData.Lastname}" />
```

Access a simple through the collection:
```
<(...) Text="{Binding CollectionView/BindingData.Lastname}" />
```

Bind to the collection:
```
<(...) ItemsSource="{Binding CollectionView}" />
```

More samples will come ;)
