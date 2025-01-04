// PlayerListPage.tsx
import React, { useEffect, useState } from 'react';
import { Client, FootballPlayer } from '../api/api';
import { useNavigate } from 'react-router-dom';
import { usePlayerContext } from '../context/PlayerContext';
import { BACK_ADDRESS } from '../config';
import { connection } from '../api/FootballPlayerHub';
import { deletePlayer } from '../api/FootballPlayerHub';
import './PlayerListPage.css'

const apiClient = new Client(BACK_ADDRESS);

const PlayerListPage: React.FC = () => {
  const [players, setPlayers] = useState<FootballPlayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [deletingPlayerId, setDeletingPlayerId] = useState<string | null>(null);
  const navigate = useNavigate();
  const { playerDictionary, setPlayerDictionary } = usePlayerContext();


  useEffect(() => {
    const createHubConnection = async () => {
      connection.on('AddPlayer', (player) => {
         setPlayers((prevPlayers) => [...prevPlayers, player]);
         setPlayerDictionary((prevDict) => ({ ...prevDict, [player.id]: player }));
      });
     connection.on('UpdatePlayer', (updatedPlayer) => {
           setPlayers((prevPlayers) => {
              const filteredPlayers = prevPlayers.filter((player) => player.id !== updatedPlayer.id);
              return [...filteredPlayers, updatedPlayer];
            });
            setPlayerDictionary((prevDict) => ({
              ...prevDict,
              [updatedPlayer.id]: updatedPlayer,
            }));
        });

       connection.on('DeletePlayer', (playerId) => {
          setPlayers((prevPlayers) => prevPlayers.filter((p) => p.id !== playerId));
          setPlayerDictionary((prevDict) => {
             const updatedPlayerDict = {...prevDict};
              delete updatedPlayerDict[playerId];
              return updatedPlayerDict;
          });
       });
     };
    createHubConnection();
  }, [connection]);


  useEffect(() => {
    const fetchPlayers = async () => {
      try {
        const response = await apiClient.footballPlayerAll();
        setPlayers(response);

        const playerDict = response.reduce((acc: { [key: string]: FootballPlayer }, player) => {
          acc[player.id as string] = player;
          return acc;
        }, {});
        setPlayerDictionary(playerDict);
      } catch (err) {
        setError('Не удалось загрузить список игроков');
      } finally {
        setLoading(false);
      }
    };

    fetchPlayers();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (loading) {
    return <p>Загрузка...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  const handleEditPlayer = (playerId: string | undefined) => {
    if (playerId) {
      navigate(`/update/${playerId}`);
    }
  };

  const handleDeletePlayer = async (playerId: string | undefined) => {
    if (playerId) {
        setDeletingPlayerId(playerId);
      let attempts = 0;
      const maxAttempts = 100;
      let success = false;

      while (attempts < maxAttempts && !success) {
        attempts++;
        try {
          await apiClient.footballPlayerDELETE(playerId);
             const updatedPlayers = await apiClient.footballPlayerAll();
             setPlayers(updatedPlayers);

             const playerDict = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
               acc[player.id as string] = player;
              return acc;
            }, {});
            setPlayerDictionary(playerDict);
            await deletePlayer(playerId);
          success = true;
        } catch (err) {
          console.error(`Error deleting player (attempt ${attempts}):`, err);
          await new Promise(resolve => setTimeout(resolve, 100));
        }
      }
       setDeletingPlayerId(null);
      if (!success) {
        setError('Не удалось удалить игрока после нескольких попыток');
      }
    }
  };


  return (
    <div>
      <h1>Список игроков</h1>
      {players.length === 0 ? (
        <p>Нет игроков для отображения</p>
      ) : (
        <ul>
          {players.slice().reverse().map((player) => (
            <li key={player.id}>
              <div>
                <h2>
                  {player.firstName} {player.lastName}
                </h2>
                <p>Команда: {player.teamName}</p>
                <p>Страна: {player.country}</p>
                <p>Дата рождения: {player.dateOfBirth?.toString()}</p>
                <p>Пол: {player.paul}</p>
                  <p>Дата создания: {player.creationDate?.toString()}</p>
                  <p>Дата редактирования: {player.editDate?.toString()}</p>
              </div>
              <div>
                <button onClick={() => handleEditPlayer(player.id)}>Редактировать</button>
                  <button
                  onClick={() => handleDeletePlayer(player.id)}
                  disabled={deletingPlayerId === player.id}
                  >
                    {deletingPlayerId === player.id ? "Удаление..." : "Удалить"}
                  </button>
                </div>
            </li>
          ))}
        </ul>

      )}
    </div>
  );
};

export default PlayerListPage;

