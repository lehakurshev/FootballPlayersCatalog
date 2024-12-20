// PlayerListPage.tsx
import React, { useEffect, useState } from 'react';
import { Client, FootballPlayer } from '../api/api';
import { useNavigate } from 'react-router-dom';
import { usePlayerContext } from '../context/PlayerContext';
import { BACK_ADDRESS } from '../config';
import { connection, startConnection } from '../api/FootballPlayerHub';
import { deletePlayer } from '../api/FootballPlayerHub';
import './PlayerListPage.css'

const apiClient = new Client(BACK_ADDRESS);

const PlayerListPage: React.FC = () => {
  const [players, setPlayers] = useState<FootballPlayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const { playerDictionary, setPlayerDictionary } = usePlayerContext();

  useEffect(() => {
    startConnection();
  }, []);

  useEffect(() => {
    const createHubConnection = async () => {
      connection.on('AddPlayer', (player: FootballPlayer) => {
        setPlayers([...players, player]); 
        const playerDict = { ...playerDictionary, [player.id as string]: player }; 
        setPlayerDictionary(playerDict);
      });
    };

    connection.on('UpdatePlayer', (updatedPlayer: FootballPlayer) => {
      setPlayers((prevPlayers) => {
        // Удаляем обновленного игрока из списка
        const filteredPlayers = prevPlayers.filter((player) => player.id !== updatedPlayer.id);
        // Добавляем обновленного игрока в конец списка
        return [...filteredPlayers, updatedPlayer];
      });
    
      setPlayerDictionary((prevDict) => ({
        ...prevDict,
        [updatedPlayer.id as string]: updatedPlayer,
      }));
    });

  connection.on('DeletePlayer', (playerId: string) => {
    const updatedPlayers = players.filter((p) => p.id !== playerId);
    setPlayers(updatedPlayers);
    const updatedPlayerDict = {...playerDictionary};
    delete updatedPlayerDict[playerId]; 
    setPlayerDictionary(updatedPlayerDict);
  });

    createHubConnection();
  }, [players, setPlayerDictionary, playerDictionary]);

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
  }, [setPlayerDictionary]);

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
      try {
        await apiClient.footballPlayerDELETE(playerId);
        const updatedPlayers = await apiClient.footballPlayerAll();
        setPlayers(updatedPlayers);

        const playerDict = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
          acc[player.id as string] = player;
          return acc;
        }, {});
        setPlayerDictionary(playerDict);
        deletePlayer(playerId);
      } catch (err) {
        console.error("Error deleting player:", err);
        setError('Не удалось удалить игрока');
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
                {players.slice().reverse().map((player) => ( // Создаем новый массив и разворачиваем его
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
                            <button onClick={() => handleDeletePlayer(player.id)}>Удалить</button>
                        </div>
                    </li>
                ))}
            </ul>
        )}
    </div>
);
};

export default PlayerListPage;

