// src/App.tsx

import React, { useState } from 'react';
import AddPlayer from './components/AddPlayer';
import PlayerListPage from './pages/PlayerListPage';

const App: React.FC = () => {
  const [isAddPlayer, setIsAddPlayer] = useState(true);

  const togglePage = () => {
    setIsAddPlayer(!isAddPlayer);
  };

  return (
    <div>
      <button onClick={togglePage}>
        {isAddPlayer ? 'Перейти к списку игроков' : 'Добавить игрока'}
      </button>
      {isAddPlayer ? <AddPlayer /> : <PlayerListPage />}
    </div>
  );
};

export default App;
