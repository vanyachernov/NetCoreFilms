import { Box } from "@chakra-ui/react";
import Header from "@/components/Header.tsx";
import FilmsTable from "@/components/FilmsTable.tsx";
import { useEffect, useState } from "react";
import {AddFilm, DeleteFilm, Film, FilmRequest, GetFilms, UpdateFilm} from "@/shared/apis/filmApi.ts";
import { Spinner } from "@chakra-ui/react"
import CreateUpdateFilm, {Mode} from "@/components/CreateUpdateFilm.tsx";
import DeleteFilmAlert from "@/components/DeleteFilmAlert.tsx";
import "./App.css"

function App() {
    const [films, setFilms] = useState<Film[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [modalMode, setModalMode] = useState<Mode>(Mode.Create);
    const [selectedFilm, setSelectedFilm] = useState<Film | null>(null);

    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState<boolean>(false);
    const [filmToDelete, setFilmToDelete] = useState<string | null>(null);
    
    useEffect(() => {
        const fetchFilms = async () => {
            const filmsData = await GetFilms();
            
            setFilms(filmsData.films);
            setLoading(false);
        };
        
        fetchFilms();
    }, []);

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
        
        closeModal();
    };

    const handleUpdate = async (id: string, request: FilmRequest) => {
        await UpdateFilm(id, request);

        const filmsData = await GetFilms();
        setFilms(filmsData.films);
        
        closeModal();
    };

    const handleDelete = async (filmId: string) => {
        await DeleteFilm(filmId);
        
        setFilms(films.filter((film) => film.id !== filmId));
        
        closeDeleteModal();
    };

    return (
        <Box className="bg-white text-black min-h-screen">
            <Header onNewFilm={() => openModal(Mode.Create)} />
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
                                films={films} 
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
