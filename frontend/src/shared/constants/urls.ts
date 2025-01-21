export const BASE_URL = import.meta.env.VITE_BASE_BACKEND_URL;

const urls = {
    FILMS: {
        GET: `${BASE_URL}/films`,
        GET_BY_ID: `${BASE_URL}/films/:filmId`,
        CREATE: `${BASE_URL}/films`,
        UPDATE: `${BASE_URL}/films/:filmId`,
        DELETE: `${BASE_URL}/films/:filmId`,
    },
};

export default urls;