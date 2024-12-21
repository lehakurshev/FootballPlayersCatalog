import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { FootballPlayer } from '../api/api';
import { BACK_ADDRESS } from '../config';


// Создание соединения с хабом
export const connection: HubConnection = new HubConnectionBuilder()
    .withUrl(`${BACK_ADDRESS}/players`) // укажите правильный URL хаба
    .build();

// Метод для добавления игрока
export async function addPlayer(player: FootballPlayer): Promise<void> {
    try {
        await connection.invoke('AddPlayer', player);
    } catch (error) {
        console.error('Ошибка при добавлении игрока:', error);
    }
}

export async function updatePlayer(player: FootballPlayer): Promise<void> {
    try {
        await connection.invoke('UpdatePlayer', player);
    } catch (error) {
        console.error('Ошибка при добавлении игрока:', error);
    }
}


// Метод для удаления игрока
export async function deletePlayer(playerId: string): Promise<void> {
    try {
        await connection.invoke('DeletePlayer', playerId);
    } catch (error) {
        console.error('Ошибка при удалении игрока:', error);
    }
}

export async function startConnection(): Promise<void> {
    try {
        await connection.start();
        console.log('Соединение с хабом установлено.');
    } catch (error) {
        console.error('Ошибка при установлении соединения с хабом:', error);
    }
}
