import axios from 'axios'

const api = axios.create({

    baseURL: 'http://localhost:5013/api',
})

export default api