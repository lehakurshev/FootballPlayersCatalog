import React, { useState } from 'react';
import { Client, AddFootballPlayerDto, FootballPlayer } from '../api/api';
import { useNavigate } from 'react-router-dom';
import { addPlayer } from '../api/FootballPlayerHub';
import { BACK_ADDRESS } from '../config';
import './CommandPlayer.css';


const countries = ['Россия', 'США', 'Италия'];

const AddPlayer: React.FC = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [gender, setGender] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [teamName, setTeamName] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [country, setCountry] = useState(countries[0]);
  const [firstNameError, setFirstNameError] = useState(false);
  const [lastNameError, setLastNameError] = useState(false);
  const [genderError, setGenderError] = useState(false);
  const [dateOfBirthError, setDateOfBirthError] = useState(false);
  const [teamNameError, setTeamNameError] = useState(false);
  const navigate = useNavigate();

  const apiClient = new Client(BACK_ADDRESS);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    // Validation
    setFirstNameError(!firstName);
    setLastNameError(!lastName);
    setGenderError(!gender);
    setDateOfBirthError(!dateOfBirth);
    setTeamNameError(!teamName);

    if (!firstName || !lastName || !gender || !dateOfBirth || !teamName) {
      return;
    }

    setIsLoading(true);
    setError(null);
    const player: AddFootballPlayerDto = {
      firstName,
      lastName,
      paul: gender,
      dateOfBirth: new Date(dateOfBirth),
      teamName,
      country
    };

    let attempts = 0;
    const maxAttempts = 100;
    let success = false;

    while (attempts < maxAttempts && !success) {
      try {
        const id = await apiClient.footballPlayerPOST(player);
        const playerToAdd: FootballPlayer = {
          id,
          firstName,
          lastName,
          paul: gender,
          dateOfBirth: new Date(dateOfBirth),
          teamName,
          country
        };
        addPlayer(playerToAdd);
        navigate('/players', { replace: true });
        success = true; 
      } catch (error: any) {
        attempts += 1;
        if (attempts >= maxAttempts) {
          //setError(error.message || 'Failed to add player after multiple attempts.');
          navigate('/players');
          window.location.reload();
        }
      }
    }

    setIsLoading(false);
  };

  return (
    <form onSubmit={handleSubmit} className="add-player-form">

      {firstNameError && <span className="error-message">Поле обязательно для заполнения</span>}
      <input
        type="text"
        placeholder="Имя"
        id="firstName"
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
        onBlur={() => setFirstNameError(!firstName)}
        className={firstNameError ? 'error' : ''}
      />


      {lastNameError && <span className="error-message">Поле обязательно для заполнения</span>}
      <input
        type="text"
        placeholder="Фамилия"
        id="lastName"
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
        onBlur={() => setLastNameError(!lastName)}
        className={lastNameError ? 'error' : ''}
      />

      {genderError && <span className="error-message">Поле обязательно для заполнения</span>}
      <select
        id="gender"
        value={gender}
        onChange={(e) => setGender(e.target.value)}
        onBlur={() => setGenderError(!gender)}
        className={genderError ? 'error' : ''}
      >
        <option value="">Выберите пол</option>
        <option value="male">Мужской</option>
        <option value="female">Женский</option>
      </select>
      {dateOfBirthError && <span className="error-message">Поле обязательно для заполнения</span>}
      <input
        type="date"
        id="dateOfBirth"
        value={dateOfBirth}
        onChange={(e) => setDateOfBirth(e.target.value)}
        onBlur={() => setDateOfBirthError(!dateOfBirth)}
        className={dateOfBirthError ? 'error' : ''}
      />
      {teamNameError && <span className="error-message">Поле обязательно для заполнения</span>}
      <input
        type="text"
        placeholder="Название команды"
        id="teamName"
        value={teamName}
        onChange={(e) => setTeamName(e.target.value)}
        onBlur={() => setTeamNameError(!teamName)}
        className={teamNameError ? 'error' : ''}
      />
      <select id="country" value={country} onChange={(e) => setCountry(e.target.value)}>
        {countries.map((country) => (
          <option key={country} value={country}>
            {country}
          </option>
        ))}
      </select>

      <button type="submit" disabled={isLoading}>
        {isLoading ? 'Adding...' : 'Добавить'}
      </button>
      {error && <p className="error-message">{error}</p>}
    </form>
  );
};

export default AddPlayer;
