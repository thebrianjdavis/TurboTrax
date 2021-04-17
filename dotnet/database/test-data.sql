USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

--Table to hold event playlists
CREATE TABLE playlists (
	playlist_id int IDENTITY(1,1) NOT NULL,
	playlist_name varchar(50) NOT NULL,
	CONSTRAINT PK_playlist PRIMARY KEY (playlist_id)
);

CREATE TABLE events (
	event_id int IDENTITY(1,1) NOT NULL,
	dj_user_id int NOT NULL,
	host_user_id int,
	playlist_id int NOT NULL,
	event_name varchar(50) NOT NULL,
	description varchar(255) NOT NULL,
	event_date date,
	start_time time,
	end_time time,
	CONSTRAINT PK_event PRIMARY KEY (event_id),
	CONSTRAINT FK_event_dj FOREIGN KEY (dj_user_id) REFERENCES users(user_id),
	--CONSTRAINT FK_event_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id)
);

CREATE TABLE excluded_genres (
	event_id int NOT NULL,
	genre varchar(50) NOT NULL
	CONSTRAINT PK_excluded_genres PRIMARY KEY (event_id, genre)
);

CREATE TABLE songs (
	song_id int IDENTITY(1,1) NOT NULL,
	song_name varchar(255) NOT NULL,
	artist_name varchar(255) NOT NULL,
	genre varchar(50) NOT NULL,
	spotify_id varchar(50) NOT NULL,
	img_url varchar(255),
	CONSTRAINT PK_song PRIMARY KEY (song_id)
);

--Table of event playlist songs
CREATE TABLE playlist_songs (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	CONSTRAINT PK_playlist_song PRIMARY KEY (playlist_id, song_id),
	CONSTRAINT FK_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_song_id FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

CREATE TABLE playlist_songs_shoutouts (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	shoutout_id int IDENTITY(1,1) NOT NULL,
	shoutout_message varchar(255),
	CONSTRAINT PK_playlist_song_shoutouts PRIMARY KEY (playlist_id, song_id, shoutout_id),
	CONSTRAINT FK_playlist_id_shoutouts FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_song_id_shoutouts FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

CREATE TABLE potential_playlist_songs (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	song_name varchar(255) NOT NULL,
	artist_name varchar(255) NOT NULL,
	genre varchar(50) NOT NULL,
	spotify_id varchar(50) NOT NULL,
	img_url varchar(255),
	song_score int DEFAULT 0,
	CONSTRAINT PK_potential_playlist_song PRIMARY KEY (playlist_id, song_id),
	CONSTRAINT FK_potential_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_potential_song_id FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

/* All data above this line must match capstone.sql */

INSERT INTO users(username, first_name, last_name, password_hash, salt, user_role)
VALUES ('tester', 'Bob', 'Testerson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('tester2', 'Janet', 'Testperson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'host'),
('murphy', 'Zak', 'Murphy', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('bressler', 'Brooklyn', 'Bressler', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('morgan', 'David', 'Morgan', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('davis', 'Brian', 'Davis', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj');

INSERT INTO songs(song_name, artist_name, genre, spotify_id, img_url)
VALUES ('Boot Scootin Boogie', 'Brooks & Dunn', 'Country', '7Fq9RwQxSn3kW85PrDUf0M', 'https://i.scdn.co/image/ab67616d0000b273941001e5ce6bc1272466de29'),
('Neon Moon', 'Brooks & Dunn', 'Country', '3EUl8M6SzxZl03NPkB8mUd', 'https://i.scdn.co/image/ab67616d0000b273941001e5ce6bc1272466de29'),
('My Maria', 'Brooks & Dunn', 'Country', '09qzRI951OVkXGCc33gzcT', 'https://i.scdn.co/image/ab67616d0000b273941001e5ce6bc1272466de29'),
('Drinkin Problem', 'Midland', 'Country', '1vBpwxsLVwaR4evBIaFmqD', 'https://i.scdn.co/image/ab67616d0000b27308c3459da0ec44b7e790555d'),
('Fourteen Gears', 'Midland', 'Country', '6oQZavCjiMN7q2tGmK8K2F', 'https://i.scdn.co/image/ab67616d0000b273f7453f2aad02bad5cb1fd209'),
('Thriller', 'Michael Jackson', 'Pop', '2LlQb7Uoj1kKyGhlkBf9aC', 'https://i.scdn.co/image/ab67616d0000b273de437d960dda1ac0a3586d97'),
('Billie Jean', 'Michael Jackson', 'Pop', '5ChkMS8OtdzJeqyybCc9R5', 'https://i.scdn.co/image/ab67616d0000b2734121faee8df82c526cbab2be'),
('Beat It', 'Michael Jackson', 'Pop', '1OOtq8tRnDM8kG2gqUPjAj', 'https://i.scdn.co/image/ab67616d0000b2734121faee8df82c526cbab2be'),
('Man In The Mirror', 'Michael Jackson', 'Pop', '3c7Ctlw9MKlIQPxRH3fOTt', 'https://i.scdn.co/image/ab67616d0000b27362e97ae5072de10850578af5'),
('Smells Like Teen Spirit', 'Nirvana', 'Alternative', '5ghIJDpPoe3CfHMGu71E6T', 'https://i.scdn.co/image/ab67616d0000b273e175a19e530c898d167d39bf'),
('Come As You Are', 'Nirvana', 'Alternative', '4P5KoWXOxwuobLmHXLMobV', 'https://i.scdn.co/image/ab67616d0000b273e175a19e530c898d167d39bf'),
('Lithium', 'Nirvana', 'Alternative', '2YodwKJnbPyNKe8XXSE9V7', 'https://i.scdn.co/image/ab67616d0000b273e175a19e530c898d167d39bf'),
('I Want It That Way', 'Backstreet Boys', 'Pop', '47BBI51FKFwOMlIiX6m8ya', 'https://i.scdn.co/image/ab67616d0000b2732160c02bc56f192df0f4986b'),
('Everybody', 'Backstreet Boys', 'Pop', '5WTxbyWTpoqhdxEN2szOnl', 'https://i.scdn.co/image/ab67616d0000b273dafd4b9261a1ab9acd53a53d'),
('As Long As You Love Me', 'Backstreet Boys', 'Pop', '00WvmRXTkPBZNhhRK3xfdy', 'https://i.scdn.co/image/ab67616d0000b273dafd4b9261a1ab9acd53a53d'),
('Wonderwall', 'Oasis', 'Alternative', '1qPbGZqppFwLwcBC1JQ6Vr', 'https://i.scdn.co/image/ab67616d0000b2732f2eeee9b405f4d00428d84c'),
('Champagne Supernova', 'Oasis', 'Alternative', '6EMynpZ10GVcwVqiLZj6Ye', 'https://i.scdn.co/image/ab67616d0000b2732f2eeee9b405f4d00428d84c'),
('Dont Look Back In Anger', 'Oasis', 'Alternative', '7ppPZa3TRUSGKaks9wH7VT', 'https://i.scdn.co/image/ab67616d0000b2732f2eeee9b405f4d00428d84c'),
('My Heart Will Go On', 'Celine Dion', 'Pop', '33LC84JgLvK2KuW43MfaNq', 'https://i.scdn.co/image/ab67616d0000b273745adc3f697ea1ec79d66901'),
('All By Myself', 'Celine Dion', 'Pop', '0gsl92EMIScPGV1AU35nuD', 'https://i.scdn.co/image/ab67616d0000b273c6aebd89b2dcda3348649633'),
('Nothing Broken But My Heart', 'Celine Dion', 'Pop', '03xpqxQ2tCUuDAKNLThmGO', 'https://i.scdn.co/image/ab67616d00001e02fb23175eac76782881ed60ed'),
('You Shook Me All Night Long', 'AC/DC', 'Rock', '2SiXAy7TuUkycRVbbWDEpo', 'https://i.scdn.co/image/ab67616d00001e020b51f8d91f3a21e8426361ae'),
('Thunderstruck', 'AC/DC', 'Rock', '57bgtoPSgt236HzfBOd8kj', 'https://i.scdn.co/image/ab67616d0000b2738399047ff71200928f5b6508'),
('Highway To Hell', 'AC/DC', 'Rock', '2zYzyRzz6pRmhPzyfMEC8s', 'https://i.scdn.co/image/ab67616d0000b27351c02a77d09dfcd53c8676d0'),
('Money', 'Pink Floyd', 'Rock', '0vFOzaXqZHahrZp6enQwQb', 'https://i.scdn.co/image/ab67616d0000b273f05e5ac32fdd79d100315a20'),
('Wish You Were Here', 'Pink Floyd', 'Rock', '6mFkJmJqdDVQ1REhVfGgd1', 'https://i.scdn.co/image/ab67616d0000b273d8fa5ac6259dba33127b398a'),
('Time', 'Pink Floyd', 'Rock', '3TO7bbrUKrOSPGRTB5MeCz', 'https://i.scdn.co/image/ab67616d0000b273f05e5ac32fdd79d100315a20'),
('Comfortably Numb', 'Pink Floyd', 'Rock', '5HNCy40Ni5BZJFw1TKzRsC', 'https://i.scdn.co/image/ab67616d0000b273288d32d88a616b9a278ddc07'),
('Stairway To Heaven', 'Led Zeppelin', 'Rock', '5CQ30WqJwcep0pYcV4AMNc', 'https://i.scdn.co/image/ab67616d0000b273c8a11e48c91a982d086afc69'),
('Whole Lotta Love', 'Led Zeppelin', 'Rock', '4Bv3Cg9FqLcencnvDvVjLk', 'https://i.scdn.co/image/ab67616d0000b273471f26b2813a74764efc6a7d'),
('Dazed And Confused', 'Led Zeppelin', 'Rock', '2uZbkta7TdIuEjqfvLGcfw', 'https://i.scdn.co/image/ab67616d0000b273471f26b2813a74764efc6a7d'),
('Kashmir', 'Led Zeppelin', 'Rock', '6K5R5Apnthfr9UvWeNWKC5', 'https://i.scdn.co/image/ab67616d0000b273d7cc1ec30765505ab862b17d'),
('Ms. Jackson', 'Outkast', 'Hip Hop', '0I3q5fE6wg7LIfHGngUTnV', 'https://i.scdn.co/image/ab67616d0000b2732350e31bc346a6c20e9de166'),
('Hey Ya', 'Outkast', 'Hip Hop', '2PpruBYCo4H7WOBJ7Q2EwM', 'https://i.scdn.co/image/ab67616d0000b2736a6387ab37f64034cdc7b367'),
('So Fresh, So Clean', 'Outkast', 'Hip Hop', '6glsMWIMIxQ4BedzLqGVi4', 'https://i.scdn.co/image/ab67616d0000b2732350e31bc346a6c20e9de166'),
('Baby Got Back', 'Sir Mix-a-Lot', 'Hip Hop', '1SAkL1mYNJlaqnBQxVZrRl', 'https://i.scdn.co/image/ab67616d0000b273f82c7e4376cf8267fb396b7d'),
('It Was A Good Day', 'Ice Cube', 'Hip Hop', '2qOm7ukLyHUXWyR4ZWLwxA', 'https://i.scdn.co/image/ab67616d0000b273994c319841a923465d62e271'),
('X Gon Give It To Ya', 'DMX', 'Hip Hop', '1zzxoZVylsna2BQB65Ppcb', 'https://i.scdn.co/image/ab67616d0000b27301a6faf91e207748e8e32e0a'),
('Ruff Ryders Anthem', 'DMX', 'Hip Hop', '1BKT2I9x4RGKaKqW4up34s', 'https://i.scdn.co/image/ab67616d0000b27311658b970e0518be491871df'),
('Where The Hood At', 'DMX', 'Hip Hop', '6RUpJeXT6U4SPEafnr9Wr7', 'https://i.scdn.co/image/ab67616d0000b273b6afa5d2bfa63cb0eaa386b1'),
('Dont Speak', 'No Doubt', 'Alternative', '6urCAbunOQI4bLhmGpX7iS', 'https://i.scdn.co/image/ab67616d0000b2736ebd5e789646a833b8f7d4ba'),
('Creep', 'Radiohead', 'Alternative', '70LcF31zb1H0PyJoS1Sx1r', 'https://i.scdn.co/image/ab67616d0000b273df55e326ed144ab4f5cecf95'),
('Waterfalls', 'TLC', 'Pop', '6qspW4YKycviDFjHBOaqUY', 'https://i.scdn.co/image/ab67616d0000b273a6125b1964a555892c49ea53'),
('No Scrubs', 'TLC', 'Pop', '1KGi9sZVMeszgZOWivFpxs', 'https://i.scdn.co/image/ab67616d0000b27361ffafd5e31a37336531cf95'),
('Africa', 'Toto', 'Rock', '2374M0fQpWi3dLnB54qaLX', 'https://i.scdn.co/image/ab67616d0000b2734a052b99c042dc15f933145b'),
('Material Girl', 'Madonna', 'Pop', '22sLuJYcvZOSoLLRYev1s5', 'https://i.scdn.co/image/ab67616d0000b2738f9f218b386e6aeb27184307'),
('Whats Love Got To Do With It', 'Tina Turner', 'Pop', '4kOfxxnW1ukZdsNbCKY9br', 'https://i.scdn.co/image/ab67616d0000b273e9c361da971c6e81b51ef06b');

INSERT INTO playlists (playlist_name)
VALUES ('Party with Brooklyn'), ('Zak Shack');

INSERT INTO events (dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time)
VALUES (6, 4, 1, 'Party with Brooklyn', 'Aint no party like a Brooklyn party cause a Brooklyn party dont stop!', '2021-04-07', '17:00', '18:00'),
	   (6, 3, 2, 'Zak Shack', 'Zak loves Toto!!!!', '2021-04-08', '19:00', '20:00');

INSERT INTO playlist_songs (playlist_id, song_id)
VALUES (1, 12), (1, 13), (1, 14), (1, 15), (1, 16), (1, 17), (1, 18), (1, 19), (1, 20), (1, 21),
(2, 45);

--INSERT INTO excluded_genres (event_id, genre)
--VALUES (2, 'Country'), (2, 'Alternative');