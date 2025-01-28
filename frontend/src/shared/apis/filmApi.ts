import axios from "axios";
import urls from "@/shared/constants/urls.ts";

export interface Film {
    id: string;
    fullName: {
        name: string;
        description: string;
    };
    genre: {
        title: string;
    };
    director: {
        fullName: string;
    };
    rating: {
        points: number;
    };
    release: {
        year: number;
    };
}

export interface FilmRequest {
    fullName: {
        name: string;
        description: string;
    };
    genre: {
        title: string;
    };
    director: {
        fullName: string;
    };
    rating: {
        points: number;
    };
    release: {
        year: number;
    };
}

interface FilmsApiResponse {
    result: {
        films: Film[]
    };
}

export const GetFilms = async (): Promise<{ films: Film[] }> => {
    try {
        const response = await axios.get<FilmsApiResponse>(urls.FILMS.GET);

        if (response.data.result.films) {
            return {
                films: response.data.result.films.map((film: Film) => ({
                    id: film.id,
                    fullName: {
                        name: film.fullName.name,
                        description: film.fullName.description,
                    },
                    genre: {
                        title: film.genre.title,
                    },
                    director: {
                        fullName: film.director.fullName,
                    },
                    rating: {
                        points: film.rating.points,
                    },
                    release: {
                        year: film.release.year,
                    },
                })),
            };
        }

        return { films: [] };
    } catch (error) {
        console.error("Error fetching films!", error);
        
        return { films: [] };
    }
};

export const GetFilteredFilms = async (
    title: string, 
    director: string, 
    isRatingDescending: boolean): Promise<{ films: Film[] }> => {
    try {
        title.toLowerCase();
        director.toLowerCase();
        
        const response = await axios.get(urls.FILMS.GET, {
            params: { title, director, isRatingDescending }
        });

        return response.data.result;
    } catch (error) {
        console.error("Error fetching films!", error);
        return { films: [] };
    }
};

export const AddFilm = async (filmRequest: FilmRequest) => {
    try {
        await axios.post(urls.FILMS.CREATE, filmRequest);
    } catch (error) {
        console.error("Error adding film!", error);
    }
};

export const UpdateFilm = async(id: string, filmRequest: FilmRequest) => {
    try {
        const updatedUrl = urls.FILMS.UPDATE.replace(":filmId", id);
        
        await axios.put(updatedUrl, filmRequest);
    } catch (error) {
        console.error("Error updating film!", error);
    }
}

export const DeleteFilm = async(id: string) => {
    try {
        const updatedUrl = urls.FILMS.DELETE.replace(":filmId", id);

        await axios.delete(updatedUrl);
    } catch (error) {
        console.error("Error updating film!", error);
    }
}
