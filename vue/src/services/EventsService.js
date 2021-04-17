import axios from 'axios';

export default {
    getEvents() {
        return axios.get('/events')
    },

    getEvent(id) {
        return axios.get(`/events/${id}`)
    },

    addEvent(newEvent) {
        return axios.post('/create-event', newEvent)
    },

    updateEvent(id, updatedEvent) {
        return axios.put(`/update-event/${id}`, updatedEvent)
    },

    getEventShoutouts(eventId) {
        return axios.get(`/song-shoutouts/${eventId}`)
    },
}