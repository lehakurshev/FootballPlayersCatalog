import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import AddPlayer from './components/AddPlayer';
import PlayerListPage from './pages/PlayerListPage';
import UpdatePlayer from './components/UpdatePlayer';
import { PlayerProvider } from './context/PlayerContext';
import './App.css'

const App: React.FC = () => {
  return (
    <PlayerProvider>
      <Router>
        <div>
          <nav>
            <Link to="/add">Добавить игрока</Link>
            <br />
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
