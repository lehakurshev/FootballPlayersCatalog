// src/components/AddPlayer.tsx

import React, { useState } from 'react';
import {useParams} from "react-router-dom";
import { Client, UpdateFootballPlayerDto } from '../api/api';

const countries = ['Россия', 'США', 'Италия'];

const UpdatePlayer: React.FC = () => {
    const {id} = useParams<{ id: string }>();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [paul, setGender] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [teamName, setTeamName] = useState('');
    const [country, setCountry] = useState(countries[0]);

    const apiClient = new Client('https://localhost:44307');

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const player: UpdateFootballPlayerDto = {
            id,
            firstName,
            lastName,
            paul,
            dateOfBirth: new Date(dateOfBirth),
            teamName,
            country
        };
        apiClient.footballPlayerPUT(player);
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
            <button type="submit">Изменить игрока</button>
        </form>
    );
};

export default UpdatePlayer;