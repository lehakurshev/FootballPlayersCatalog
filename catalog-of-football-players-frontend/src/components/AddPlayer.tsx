import React, { useState } from 'react';
import { Client, AddFootballPlayerDto } from '../api/api';
import { useNavigate } from 'react-router-dom';
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

  const apiClient = new Client('http://localhost:8080');

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
    try {
      await apiClient.footballPlayerPOST(player);
      navigate('/players', { replace: true });
    } catch (error: any) {
      setError(error.message || 'Failed to add player.');
    } finally {
      setIsLoading(false);
    }
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
        {isLoading ? 'Adding...' : 'Add Player'}
      </button>
      {error && <p className="error-message">{error}</p>}
    </form>
  );
};

export default AddPlayer;
