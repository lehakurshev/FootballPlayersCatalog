import React, { useState, useEffect, useRef } from 'react';
import { useParams } from "react-router-dom";
import { Client, UpdateFootballPlayerDto, FootballPlayer, Team } from '../api/api';
import { usePlayerContext } from '../context/PlayerContext';
import { useNavigate } from 'react-router-dom';
import { BACK_ADDRESS } from '../config';
import { updatePlayer } from '../api/FootballPlayerHub';

const countries = ['Россия', 'США', 'Италия'];

const EditPlayer: React.FC = () => {
    const { playerDictionary, setPlayerDictionary } = usePlayerContext();
    const { id } = useParams<{ id: string }>();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [paul, setGender] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [Teams, setTeams] = useState<Team[]>([]);
    const [teamName, setTeamName] = useState('');
    const [newTeamName, setNewTeamName] = useState('');
    const [isNewTeam, setIsNewTeam] = useState(false);
    const [country, setCountry] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [firstNameError, setFirstNameError] = useState(false);
    const [lastNameError, setLastNameError] = useState(false);
    const [genderError, setGenderError] = useState(false);
    const [dateOfBirthError, setDateOfBirthError] = useState(false);
    const [teamNameError, setTeamNameError] = useState(false);
    const navigate = useNavigate();
    const apiClient = new Client(BACK_ADDRESS);
    const isDataLoaded = useRef(false);

    useEffect(() => {
        const loadPlayerData = async () => {
            if (id && playerDictionary && playerDictionary[id]) {
                setFirstName(playerDictionary[id].firstName as string);
                setLastName(playerDictionary[id].lastName as string);
                setGender(playerDictionary[id].paul as string);
                setDateOfBirth(playerDictionary[id].dateOfBirth?.toString().slice(0, 10) || '');
                setTeamName(playerDictionary[id].teamName as string);
                setCountry(playerDictionary[id].country as string);
            } else {
                if (!isDataLoaded.current && id) {
                    isDataLoaded.current = true;
                    const player = (await apiClient.footballPlayerGET(id as string));
                    setFirstName(player.firstName as string);
                    setLastName(player.lastName as string);
                    setGender(player.paul as string);
                    setDateOfBirth(player.dateOfBirth?.toString().slice(0, 10) || '');
                    setTeamName(player.teamName as string);
                    setCountry(player.country as string);
                    console.log(player);
                }
            }
            setIsLoading(false);
        };
        if (id) {
            loadPlayerData();
        }
    }, [id, playerDictionary]);


    useEffect(() => {
        const fetchTeams = async () => {
            let attempts = 0;
            const maxAttempts = 100;
            let success = false;

            while (attempts < maxAttempts && !success) {
                try {
                    const response = await apiClient.team();
                    setTeams(response);

                    success = true;
                } catch (err) {
                    attempts += 1;
                    if (attempts >= maxAttempts) {
                        window.location.reload();
                    }
                }
            }
        };

        fetchTeams();
    }, []);

    const handleTeamChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setTeamName(e.target.value);
        setIsNewTeam(e.target.value === 'new'); // If "New Team" is selected
    };

    const handleNewTeamNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setNewTeamName(e.target.value);
        setTeamName(e.target.value); // Keep the main teamName updated as the new team name is typed
    };


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        // Validation
        setFirstNameError(!firstName);
        setLastNameError(!lastName);
        setGenderError(!paul);
        setDateOfBirthError(!dateOfBirth);
        setTeamNameError(!teamName);

        if (!firstName || !lastName || !paul || !dateOfBirth || !teamName) {
            return;
        }

        setIsLoading(true);
        setError(null);

        const player: UpdateFootballPlayerDto = {
            id: id as string,
            firstName,
            lastName,
            paul,
            dateOfBirth: new Date(dateOfBirth),
            teamName,
            country
        };

        let attempts = 0;
        const maxAttempts = 100;
        let success = false;

        while (attempts < maxAttempts && !success) {
            try {
                await apiClient.footballPlayerPUT(player);
                const updatedPlayers = await apiClient.footballPlayerAll();
                const newPlayerDictionary = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
                    acc[player.id as string] = player;
                    return acc;
                }, {});
                setPlayerDictionary(newPlayerDictionary);
                const playerToAdd: FootballPlayer = {
                    id,
                    firstName,
                    lastName,
                    paul: paul,
                    dateOfBirth: new Date(dateOfBirth),
                    teamName,
                    country
                };
                updatePlayer(playerToAdd);
                success = true;
                navigate('/players', { replace: true });
            } catch (err: any) {
                attempts += 1;
                if (attempts >= maxAttempts) {
                    navigate('/players');
                    window.location.reload();
                }
            }
        }

        setIsLoading(false);
    };


    if (isLoading) {
        return <p>Updating player...</p>;
    }

    if (error) {
        return <p>Error: {error}</p>;
    }


    return (
        <form onSubmit={handleSubmit}>
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
                value={paul}
                onChange={(e) => setGender(e.target.value)}
                onBlur={() => setGenderError(!paul)}
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
            <select
                id="teamName"
                value={teamName}
                onChange={handleTeamChange}
                onBlur={() => setTeamNameError(!teamName)}
                className={teamNameError ? 'error' : ''}
            >
                <option value="">Выберите команду</option>
                {Teams.map((team) => (
                    <option key={team.id} value={team.name}>
                        {team.name}
                    </option>
                ))}
                <option value="new">Новая команда</option>
            </select>

            {isNewTeam && (
                <input
                    type="text"
                    placeholder="Название новой команды"
                    value={newTeamName}
                    onChange={handleNewTeamNameChange}
                    className="new-team-input"
                />
            )}



            <select id="country" value={country} onChange={(e) => setCountry(e.target.value)}>
                {countries.map((country) => (
                    <option key={country} value={country}>
                        {country}
                    </option>
                ))}
            </select>

            <button type="submit" disabled={isLoading}>
                {isLoading ? 'Adding...' : 'Изменить'}
            </button>
            {error && <p className="error-message">{error}</p>}
        </form>
    );
};

export default EditPlayer;
