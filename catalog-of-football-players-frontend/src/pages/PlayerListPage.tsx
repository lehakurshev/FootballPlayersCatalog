// src/pages/PlayerListPage.tsx
import React, { useEffect, useState } from 'react';
import { Client, FootballPlayer } from '../api/api';

const apiClient = new Client('https://localhost:44307');

const PlayerListPage: React.FC = () => {
  const [players, setPlayers] = useState<FootballPlayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchPlayers = async () => {
      try {
        const response = await apiClient.footballPlayerAll();
        setPlayers(response);
      } catch (err) {
        setError('Не удалось загрузить список игроков');
      } finally {
        setLoading(false);
      }
    };

    fetchPlayers();
  }, []);

  if (loading) {
    return <p>Загрузка...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  return (
    <div>
      <h1>Список игроков</h1>
      {players.length === 0 ? (
        <p>Нет игроков для отображения</p>
      ) : (
        <ul>
          {players.map((player) => (
            <li key={player.id}> {/* Предполагается, что у игрока есть уникальный идентификатор `id` */}
              {player.firstName} {player.lastName} - {player.teamName} ({player.country})
              {/* Вы можете добавить здесь кнопку редактирования или удаления */}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default PlayerListPage;
