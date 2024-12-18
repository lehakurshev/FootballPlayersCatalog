import React, { useState, useEffect, useRef } from 'react';
import { useParams } from "react-router-dom";
import { Client, UpdateFootballPlayerDto, FootballPlayer } from '../api/api';
import { usePlayerContext } from '../context/PlayerContext';
import { useNavigate } from 'react-router-dom';

const countries = ['Россия', 'США', 'Италия'];

const EditPlayer: React.FC = () => {
    const { playerDictionary, setPlayerDictionary } = usePlayerContext();
    const { id } = useParams<{ id: string }>();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [paul, setGender] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [teamName, setTeamName] = useState('');
    const [country, setCountry] = useState('');
    const [isLoading, setIsLoading] = useState(false); 
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();
    const apiClient = new Client('http://localhost:8080');
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


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
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

        try {
            await apiClient.footballPlayerPUT(player);
            const updatedPlayers = await apiClient.footballPlayerAll();
            const newPlayerDictionary = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
                acc[player.id as string] = player;
                return acc;
            }, {});
            setPlayerDictionary(newPlayerDictionary);

            navigate('/players', { replace: true });
        } catch (err: any) {
            setError(err.message || 'An error occurred while updating the player.');
        } finally {
            setIsLoading(false);
        }
    };

    if (isLoading) {
        return <p>Updating player...</p>;
    }

    if (error) {
        return <p>Error: {error}</p>;
    }


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
                {isLoading ? 'Updating...' : 'Update Player'}
            </button>
        </form>
    );
};

export default EditPlayer;
