import React, { useState, useEffect } from 'react';
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
    const [isLoading, setIsLoading] = useState(false); // Initially not loading
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();
    const apiClient = new Client('https://localhost:44307');

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
                setError('Player not found or context not ready.');
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
        setError(null); // Clear any previous errors

        const player: UpdateFootballPlayerDto = {
            id: id as string, // Ensure id is a string
            firstName,
            lastName,
            paul,
            dateOfBirth: new Date(dateOfBirth), // Handle empty dateOfBirth
            teamName,
            country
        };

        try {
            await apiClient.footballPlayerPUT(player);
            // Update the playerDictionary in context after successful update.
            const updatedPlayers = await apiClient.footballPlayerAll(); //refetch all players
            const newPlayerDictionary = updatedPlayers.reduce((acc: { [key: string]: FootballPlayer }, player) => {
              acc[player.id as string] = player;
              return acc;
            }, {});
            setPlayerDictionary(newPlayerDictionary);

            navigate('/players', { replace: true }); // Navigate after successful update
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
