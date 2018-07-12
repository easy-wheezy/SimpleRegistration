SQL Setup

Open Localdb instance in SSMS, and run the Setup script. (This script is rerunnable if you wish to restart)
The database and App will expect windows Auth, so should work out the box. 

Limitations/Caveats
The method I planned to do validation notifications after a successful or unsuccessful submit (with valid model) was to display the notifcation on the same page.
However, the Unobtrusive jQuery installing into dotnet core does not work, so it renders on a new page. 