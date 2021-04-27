# About Movie Tracker

Welcome to MovieTracker! This is a Xamarin Forms Android application that I have been working on since the start of this year (2021). This was created mainly to demonstrate my ability to work in the .NET and Xamarin Forms frameworks. The app was written using the MVVM structural design pattern.

# App Functionality

### What’s Popular

While simple in design, this application provides a great amount of user interaction that allows a user to maintain watchlists for movies and television shows, mark items as favorites, and even provide your own user rating. Users can save lists of popular movies and/or television shows to reference them later. Upon login, users are brought to the What’s New page, where popular shows and movies are loaded to be displayed. This page updates dynamically on a day to day basis, so checking back often can provide info on what’s been popular in the movie/tv world.

### Searching

Alternatively, users can search for a show or movie on the Search page. The application utilizes paged-loading techniques to allow for indefinite scrolling as a user searches for an item. Images are cached within the system as well for fast retrieval/loading. 

Selecting a movie or show loads a detail page containing more information on the selected media. Additional information includes the overall/average user score/rating, media certification, film runtime or season length, genres, and even the cast and crew associated. Furthermore, a Watch On tab provides up-to-date feedback on what streaming platforms the item is available to be viewed on - including where it is free to stream, rent, or even buy.

Backend functionality is mainly handled through a third party library interfacing with The Movie Database’s API (www.themoviedb.org). This handles requests for managing list and rating updates and is controlled with basic in-app login.

### Let’s Watch

The Let’s Watch page of the application utilizes the watchlist data to choose movies and/or shows for you to watch! The page lets you choose whether a movie or show (or either) will be selected from your combined watchlist, favorites and/or rated items. Have you ever had a difficult time picking something to watch, or simply couldn’t decide out of so many options? Let the app do it for you!

### Future Ideas


I have plans to keep expanding on to this as time sees fit. Some ideas I’ve got ruminating are:
Movie / Show Trailers
Recommended movies/shows based on a selected show/movie.
Integrate into Let’s Watch functionality for new picks
Person searching (cast + crew expanded details)
Tag / push notifications for movie / show updates.
iOS Platform

NOTE: For now, this is a personal project and is not available for public use. It's in a beta version currently and making it available to the public would require some extra handling of licensing / third party libraries being used. All work on this repository was done by me.
