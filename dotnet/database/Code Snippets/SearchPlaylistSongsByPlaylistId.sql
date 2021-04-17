SELECT pl.playlist_name, s.artist_name, s.song_name, s.genre
FROM playlists pl
JOIN playlist_songs ps ON ps.playlist_id = pl.playlist_id
JOIN songs s ON s.song_id = ps.song_id
WHERE pl.playlist_id = 1; --swap out with: WHERE @playlist_id;