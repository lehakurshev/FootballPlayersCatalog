// src/components/AddPlayer.tsx

import React, { useState } from 'react';
import { Client, AddFootballPlayerDto } from '../api/api';
import { useNavigate } from 'react-router-dom';

const countries = ['Россия', 'США', 'Италия'];

const AddPlayer: React.FC = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [paul, setGender] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [teamName, setTeamName] = useState('');
  const [isLoading, setIsLoading] = useState(false); // Add loading state
  const [error, setError] = useState<string | null>(null); // Add error state
  const [country, setCountry] = useState(countries[0]);
  const navigate = useNavigate();

  const apiClient = new Client('https://localhost:44307');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true); // Set loading state to true
    setError(null);     // Clear any previous errors
    const player: AddFootballPlayerDto = {
      firstName,
      lastName,
      paul,
      dateOfBirth: new Date(dateOfBirth),
      teamName,
      country
    };
    try {
      await apiClient.footballPlayerPOST(player);
      navigate('/players', { replace: true }); // Navigate after successful POST
    } catch (error: any) {
      setError(error.message || 'Failed to add player.'); // Set error state if POST fails
    } finally {
      setIsLoading(false); // Set loading state to false after API call completes
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Имя"
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
      />
      <input
        type="text"
        placeholder="Фамилия"
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
      />
      <select value={paul} onChange={(e) => setGender(e.target.value)}>
        <option value="">Выберите пол</option>
        <option value="male">Мужской</option>
        <option value="female">Женский</option>
      </select>
      <input
        type="date"
        value={dateOfBirth}
        onChange={(e) => setDateOfBirth(e.target.value)}
      />
      <input
        type="text"
        placeholder="Название команды"
        value={teamName}
        onChange={(e) => setTeamName(e.target.value)}
      />
      <select value={country} onChange={(e) => setCountry(e.target.value)}>
        {countries.map((country) => (
          <option key={country} value={country}>
            {country}
          </option>
        ))}
      </select>
      <button type="submit" disabled={isLoading}>
        {isLoading ? 'Adding...' : 'Add Player'}
      </button>
      {error && <p style={{ color: 'red' }}>{error}</p>} {/* Display error message */}
    </form>
  );
};

export default AddPlayer;
