// PlayerListPage.tsx
import React, { useEffect, useState } from 'react';
import { Client, FootballPlayer } from '../api/api';
import { useNavigate } from 'react-router-dom';
import { usePlayerContext } from '../context/PlayerContext';

const apiClient = new Client('https://localhost:44307');

const PlayerListPage: React.FC = () => {
  const [players, setPlayers] = useState<FootballPlayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const { setPlayerDictionary } = usePlayerContext(); // Получите функцию для обновления playerDictionary

  useEffect(() => {
    const fetchPlayers = async () => {
      try {
        const response = await apiClient.footballPlayerAll();
        setPlayers(response);

        // Обновляем playerDictionary в контексте
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
        
        // Обновляем playerDictionary после удаления игрока
        const playerDict = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
          acc[player.id as string] = player;
          return acc;
        }, {});
        setPlayerDictionary(playerDict);
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
          {players.map((player) => (
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

