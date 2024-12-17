import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import AddPlayer from './components/AddPlayer';
import PlayerListPage from './pages/PlayerListPage';
import UpdatePlayer from './components/UpdatePlayer';

const App: React.FC = () => {
  return (
    <Router>
      <div>
        <nav>
          <Link to="/">Добавить игрока</Link>
          <Link to="/players">Список игроков</Link>
        </nav>
        <Routes>
          <Route path="/" element={<AddPlayer />} />
          <Route path="/players" element={<PlayerListPage />} />
          <Route path="/update/:id" element={<UpdatePlayer />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
