using Observer;

WeatherStation station = new WeatherStation();

PhoneDisplay phoneDisplay = new PhoneDisplay(station);

//add subscribers to station
station.Add(phoneDisplay);


//notify subscribers that something has been changed
station.Notify();