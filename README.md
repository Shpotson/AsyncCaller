# AsyncCaller

Represents the EventHandler delegate, that can be invoked half-async. Than Invoked, EventHandler's methods run synchronized, until the specified time passes. 

## Constructors

__public AsyncCaller(EventHandler handler)__     

Initialize class instance with EventHandler instance.

+ __EventHandler handler__ - EventHandler instance

_throws AsyncCallerException if *handler* is *null*_

## Methods

__public bool Invoke(int milSec, object obj, EventArgs args)__    

Invokes EventHandler delegate in sync until the delegate methods are done or the execution waiting time is over. After timeout delegate methods run in async.
Returns true if the delegate's methods was accomlished in the allotted time, returns false if the execution waiting time is over before the end of the execution.

+ __int milSec__ - Delegate methods execution waiting time (in milsec)
+ __object obj__ - Parameter, passed to the EventHandler delegate
+ __EventArgs args__ - Parameter, passed to the EventHandler delegate

## Operations

__= AsyncCaller + AsyncCaller__

Returns new AsyncCaller instance with combined EventHandler delegates from both AsyncCaller instances. 

__= AsyncCaller + EventHandler__

Returns new AsyncCaller instance with combined EventHandler delegate and AsyncCaller instance of EventHandler.
