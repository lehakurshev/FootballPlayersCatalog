import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import AddPlayer from './components/AddPlayer';
import PlayerListPage from './pages/PlayerListPage';
import UpdatePlayer from './components/UpdatePlayer';
import { PlayerProvider } from './context/PlayerContext'; // Импортируйте ваш контекст

const App: React.FC = () => {
  return (
    <PlayerProvider> {/* Оборачиваем все в PlayerProvider */}
      <Router>
        <div>
          <nav>
            <Link to="/add">Добавить игрока</Link>
            <td></td>
            <Link to="/players">Список игроков</Link>
          </nav>
          <Routes>
            <Route path="/add" element={<AddPlayer />} />
            <Route path="/players" element={<PlayerListPage />} />
            <Route path="/update/:id" element={<UpdatePlayer />} />
          </Routes>
        </div>
      </Router>
    </PlayerProvider>
  );
};

export default App;
