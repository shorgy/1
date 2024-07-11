const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');

// Створення застосунку Express
const app = express();

// Налаштування middleware
app.use(bodyParser.json());
app.use(cors());

// Підключення до MongoDB
mongoose.connect('mongodb://localhost:27017/skyrunner', { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('Connected to MongoDB'))
    .catch(err => console.error('Could not connect to MongoDB...', err));

// Определение схемы и модели для игрока
const playerSchema = new mongoose.Schema({
    name: String,
    score: Number
});

const Player = mongoose.model('Player', playerSchema);

// Маршрут для отримання усіх гравців
app.get('/players', async (req, res) => {
    const players = await Player.find().sort({ score: -1 });
    res.send({ players }); // Возвращаем объект с массивом players
});

// Маршрут для додавання нового гравця
app.post('/players', async (req, res) => {
    let player = new Player({
        name: req.body.name,
        score: req.body.score
    });
    player = await player.save();
    res.send(player);
});
// Маршрут для видалення гравця по айди
app.delete('/players/:id', async (req, res) => {
    try {
        const player = await Player.findByIdAndDelete(req.params.id);
        if (!player) {
            return res.status(404).send('Player not found');
        }
        res.send(player);
    } catch (error) {
        res.status(500).send(error);
    }
});

// Запуск серверу
const port = process.env.PORT || 3000;
app.listen(port, () => console.log(`Listening on port ${port}...`));