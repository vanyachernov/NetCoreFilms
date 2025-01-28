import { Box } from "@chakra-ui/react";
import Header from "@/components/Header.tsx";
import FilmsTable from "@/components/FilmsTable.tsx";
import { useEffect, useState } from "react";
import {AddFilm, DeleteFilm, Film, FilmRequest, GetFilms, GetFilteredFilms, UpdateFilm} from "@/shared/apis/filmApi.ts";
import { Spinner } from "@chakra-ui/react"
import CreateUpdateFilm, {Mode} from "@/components/CreateUpdateFilm.tsx";
import DeleteFilmAlert from "@/components/DeleteFilmAlert.tsx";
import "./App.css"
import SearchField from "@/components/SearchField.tsx";

function App() {
    const [films, setFilms] = useState<Film[]>([]);
    const [filteredFilms, setFilteredFilms] = useState<Film[]>([]);
    
    const [loading, setLoading] = useState<boolean>(true);

    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [modalMode, setModalMode] = useState<Mode>(Mode.Create);
    const [selectedFilm, setSelectedFilm] = useState<Film | null>(null);

    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState<boolean>(false);
    const [filmToDelete, setFilmToDelete] = useState<string | null>(null);

    const [titleQuery, setTitleQuery] = useState<string>("");
    const [directorQuery, setDirectorQuery] = useState<string>("");
    const [isRatingDescending, setIsRatingDescending] = useState<boolean>(false);

    useEffect(() => {
        const fetchFilms = async () => {
            setLoading(true);
            const filmsData = await GetFilms();
            setFilms(filmsData.films);
            setFilteredFilms(filmsData.films);
            setLoading(false);
        };

        fetchFilms();
    }, []);
    
    useEffect(() => {
        const fetchFilteredFilms = async () => {
            setLoading(true);
            
            const filmsData = await GetFilteredFilms(titleQuery, directorQuery, isRatingDescending);
            setFilteredFilms(filmsData.films);
            
            setLoading(false);
        };

        if (titleQuery || directorQuery || isRatingDescending !== undefined) {
            fetchFilteredFilms();
        } else {
            setFilteredFilms(films);
        }
    }, [titleQuery, directorQuery, isRatingDescending, films]);

    const openModal = (mode: Mode, film: Film | null = null) => {
        setModalMode(mode);
        setSelectedFilm(film);
        setIsModalOpen(true);
    };
    
    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedFilm(null);
    };

    const openDeleteModal = (filmId: string) => {
        setFilmToDelete(filmId);
        setIsDeleteModalOpen(true);
    };

    const closeDeleteModal = () => {
        setIsDeleteModalOpen(false);
        setFilmToDelete(null);
    };
    
    const handleCreate = async (request: FilmRequest) => {
        await AddFilm(request);
        
        const filmsData = await GetFilms();
        setFilms(filmsData.films);
        setFilteredFilms(filmsData.films);
        
        closeModal();
    };

    const handleUpdate = async (id: string, request: FilmRequest) => {
        await UpdateFilm(id, request);

        const filmsData = await GetFilms();
        setFilms(filmsData.films);
        setFilteredFilms(filmsData.films);
        
        closeModal();
    };

    const handleDelete = async (filmId: string) => {
        await DeleteFilm(filmId);

        const updatedFilms = films.filter((film) => film.id !== filmId);
        setFilms(updatedFilms);
        setFilteredFilms(updatedFilms);
        
        closeDeleteModal();
    };

    return (
        <Box className="bg-white text-black min-h-screen">
            <Header onNewFilm={() => openModal(Mode.Create)} />
            <Box 
                width="100%"
                height="10%"
                display="flex"
                bgColor="#F5F5F5">
                <Box
                    padding={5}>
                    <SearchField 
                        onTitleSearch={setTitleQuery} 
                        onDirectorSearch={setDirectorQuery}
                        isRatingDescending={setIsRatingDescending} />
                </Box>
            </Box>
            <Box
                width="100%"
                height="80vh"
                display="flex"
                alignItems="center"
                justifyContent="center">
                <Box
                    width="60%">
                    {
                        loading 
                            ? <Spinner size="xl" color="teal.500" /> 
                            : <FilmsTable 
                                films={filteredFilms} 
                                onEdit={(film) => openModal(Mode.Edit, film)}
                                onDelete={(filmId) => openDeleteModal(filmId)} />
                    }
                </Box>
            </Box>
            <CreateUpdateFilm
                mode={modalMode}
                values={selectedFilm ? [selectedFilm] : []}
                isModalIs={isModalOpen}
                handleCancel={closeModal}
                handleCreate={handleCreate}
                handleUpdate={(_, request) => handleUpdate(selectedFilm?.id || "", request)} />
            <DeleteFilmAlert
                filmId={filmToDelete || ""}
                isModalIs={isDeleteModalOpen}
                handleCancel={closeDeleteModal}
                handleDelete={handleDelete} />
        </Box>
    );
}

export default App;
