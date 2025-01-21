import {Film, FilmRequest} from "@/shared/apis/filmApi.ts";
import {
    DialogActionTrigger,
    DialogBody,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogRoot,
    DialogTitle
} from "@/components/ui/dialog"
import { Field } from "@/components/ui/field"
import {useEffect, useRef, useState} from "react"
import {Button, Input, Stack} from "@chakra-ui/react";
import * as yup from "yup";

interface Props {
    mode: Mode;
    values: Film[];
    isModalIs: boolean;
    handleCancel: () => void;
    handleCreate: (request: FilmRequest) => void;
    handleUpdate: (id: string, request: FilmRequest) => void;
}

export enum Mode {
    Create,
    Edit
}

const CreateUpdateFilm = ({
        mode, 
        values,
        isModalIs,
        handleCancel, 
        handleCreate, 
        handleUpdate
    } : Props) => {
    
    const ref = useRef<HTMLInputElement>(null);
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [genre, setGenre] = useState<string>("");
    const [director, setDirector] = useState<string>("");
    const [releaseYear, setReleaseYear] = useState<number>(2000);
    const [rating, setRating] = useState<number>(1);
    const [errors, setErrors] = useState<Record<string, string>>({});
    
    useEffect(() => {
        if (mode === Mode.Edit && values.length > 0) {
            const [film] = values;
            
            setTitle(film.fullName.name);
            setDescription(film.fullName.description);
            setGenre(film.genre.title);
            setDirector(film.director.fullName);
            setReleaseYear(film.release.year);
            setRating(film.rating.points);
        }
    }, [mode, values]);
    
    const handleSave = async () => {
        const filmRequest: FilmRequest = {
            fullName: {
                name: title,
                description: description,
            },
            genre: {
                title: genre,
            },
            director: {
                fullName: director,
            },
            release: {
                year: releaseYear,
            },
            rating: {
                points: rating,
            },
        };

        if (mode === Mode.Create) {
            console.log("Test");
            handleCreate(filmRequest);
        } else if (mode === Mode.Edit && values.length > 0) {
            console.log("Updated");
            handleUpdate(values[0].id, filmRequest);
        }
    };
    
    return (
        <DialogRoot initialFocusEl={() => ref.current} 
                    open={isModalIs} 
                    onOpenChange={handleCancel}>
            <DialogContent bg="white" color="black">
                <DialogHeader>
                    <DialogTitle 
                        fontSize={20}
                        fontWeight="bold">
                        {mode === Mode.Create 
                            ? "Create New Film" 
                            : "Edit Film"}
                    </DialogTitle>
                </DialogHeader>
                <DialogBody pb="4">
                    <Stack gap="4">
                        <Field label="Title">
                            <Input
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                placeholder="Enter film title"
                            />
                        </Field>
                        <Field label="Description">
                            <Input
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                placeholder="Enter film description"
                            />
                        </Field>
                        <Field label="Genre">
                            <Input
                                value={genre}
                                onChange={(e) => setGenre(e.target.value)}
                                placeholder="Enter film genre"
                            />
                        </Field>
                        <Field label="Director">
                            <Input
                                value={director}
                                onChange={(e) => setDirector(e.target.value)}
                                placeholder="Enter director name"
                            />
                        </Field>
                        <Field label="Release Year">
                            <Input
                                type="number"
                                value={releaseYear.toString()}
                                onChange={(e) => setReleaseYear(Number(e.target.value))}
                                placeholder="Enter release year"
                            />
                        </Field>
                        <Field label="Rating">
                            <Input
                                type="number"
                                value={rating.toString()}
                                onChange={(e) => setRating(Number(e.target.value))}
                                placeholder="Enter film rating (1-10)"
                            />
                        </Field>
                    </Stack>
                </DialogBody>
                <DialogFooter gap={5}>
                    <DialogActionTrigger asChild>
                        <Button 
                            variant="outline" 
                            onClick={handleCancel}>
                            Cancel
                        </Button>
                    </DialogActionTrigger>
                    <Button 
                        bgColor="teal"
                        padding={5}
                        color="white"
                        onClick={handleSave}>
                        Save
                    </Button>
                </DialogFooter>
            </DialogContent>
        </DialogRoot>
    );
};

export default CreateUpdateFilm;