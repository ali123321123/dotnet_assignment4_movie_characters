# Dotnet_assignment4_movie_characters
###### Created by Thanh Tran, Olav Rongved and Ali.

Assignment for Noroff Dotnet programme. The purpose of the project is to make a RESTful API that can store information about characters, movies and franchises.
The Code is also made to be able to easily expand on the features where we have used the repository pattern to structure the program. 

**Programming language and tools**
* C#
* Entity framework
* Swagger
* SQL Server 
* ASP.NET Core
* RESTful API

## Business Rules

- [ ] One movie contains many character
- [ ] A character can play in multiple movies, make a linking table that can be called MovieCharacter 
- [ ] One movies belongs to one franchise
- [ ] A franchise can contain many movies


## Data requirements

**Character**

- [ ] Autoincremented id
- [ ] Full name
- [ ] Alias(if applicable)
- [ ] Gender
- [ ] Picture (URL to photo - do not store an image)

**MovieCharacter**

- [ ] PK and FK 
- [ ] Picture(URL again, for a particular movie)
- [ ] When getting all characters in a movie, this image should be shown not the base picture for a character

**Movie**

- [ ] Autoincremented Id
- [ ] Movie title
- [ ] Genre (just a simple string of a comma sepereated genres, there is no genre modelling required as a base)
- [ ] Release year
- [ ] Director(just a string name, no director modelling required as a base)
- [ ] Picture (URL to a movie poster)
- [ ] Trailer (Youtube link most likely)

**Franchise**

- [ ] Autoincremented Id
- [ ] Name
- [ ] Description

## API Requirements

Full CRUD for
- [ ] Movies
- [ ] Characters
- [ ] Franchises

- [ ] Make sure related data is not deleted - foreign keys kan be set to null
- [ ] Moviecharacter linking table have to remove the related entries when a Character or Movie is deleted

Additional endpoints 
- [ ] Get all the movies in a franchise
- [ ] Get all the characters in a movie
- [ ] Get all the caracters in a franchise 