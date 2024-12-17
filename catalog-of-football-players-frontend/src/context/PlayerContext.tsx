import React, { createContext, useContext, useState, useEffect } from 'react';
import { FootballPlayer } from '../api/api';

interface PlayerContextType {
  playerDictionary: { [key: string]: FootballPlayer };
  setPlayerDictionary: React.Dispatch<React.SetStateAction<{ [key: string]: FootballPlayer }>>;
}

const PlayerContext = createContext<PlayerContextType | undefined>(undefined);

export const PlayerProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [playerDictionary, setPlayerDictionary] = useState<{ [key: string]: FootballPlayer }>({});

  useEffect(() => {
    const storedPlayers = localStorage.getItem('playerDictionary');
    if (storedPlayers) {
      try {
        setPlayerDictionary(JSON.parse(storedPlayers));
      } catch (error) {
        console.error("Error parsing playerDictionary from localStorage:", error);
      }
    }
  }, []);

  useEffect(() => {
    localStorage.setItem('playerDictionary', JSON.stringify(playerDictionary));
  }, [playerDictionary]);

  return (
    <PlayerContext.Provider value={{ playerDictionary, setPlayerDictionary }}>
      {children}
    </PlayerContext.Provider>
  );
};

export const usePlayerContext = () => {
  const context = useContext(PlayerContext);
  if (!context) {
    throw new Error('usePlayerContext must be used within a PlayerProvider');
  }
  return context;
};

