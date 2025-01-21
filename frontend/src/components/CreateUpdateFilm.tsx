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
import {useEffect, useRef} from "react"
import {Button, Input, Stack} from "@chakra-ui/react";
import * as yup from "yup";
import {ErrorMessage, Field as FormikField, Form, Formik} from "formik";

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

    const validationSchema = yup.object().shape({
        title: yup.string().required("Title is required"),
        description: yup.string().required("Description is required"),
        genre: yup.string().required("Genre is required"),
        director: yup.string().required("Director is required"),
        releaseYear: yup
            .number()
            .required("Release Year is required")
            .min(1888, "Year must be at least 1888")
            .max(new Date().getFullYear(), "Year cannot be in the future"),
        rating: yup
            .number()
            .required("Rating is required")
            .min(1, "Rating must be at least 1")
            .max(10, "Rating must be no more than 10"),
    });
    
    const initialValues = {
        title: "",
        description: "",
        genre: "",
        director: "",
        releaseYear: 2000,
        rating: 1,
    };
    
    useEffect(() => {
        if (mode === Mode.Edit && values.length > 0) {
            const [film] = values;

            initialValues.title = film.fullName.name;
            initialValues.description = film.fullName.description;
            initialValues.genre = film.genre.title;
            initialValues.director = film.director.fullName;
            initialValues.releaseYear = film.release.year;
            initialValues.rating = film.rating.points;
        }
    }, [mode, values]);
    
    const handleSave = async (formValues: typeof initialValues) => {
        const filmRequest: FilmRequest = {
            fullName: {
                name: formValues.title,
                description: formValues.description,
            },
            genre: {
                title: formValues.genre,
            },
            director: {
                fullName: formValues.director,
            },
            release: {
                year: formValues.releaseYear,
            },
            rating: {
                points: formValues.rating,
            },
        };

        if (mode === Mode.Create) {
            handleCreate(filmRequest);
        } else if (mode === Mode.Edit && values.length > 0) {
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
                <DialogBody>
                    <Formik
                        initialValues={initialValues}
                        validationSchema={validationSchema}
                        onSubmit={handleSave}
                        enableReinitialize>
                        {({ handleSubmit, isValid, dirty }) => (
                            <Form onSubmit={handleSubmit}>
                                <Stack gap="4">
                                    <Field label="Title">
                                        <FormikField
                                            as={Input}
                                            name="title"
                                            placeholder="Enter film title"
                                        />
                                        <ErrorMessage name="title" component="div" />
                                    </Field>
                                    <Field label="Description">
                                        <FormikField
                                            as={Input}
                                            name="description"
                                            placeholder="Enter film description"
                                        />
                                        <ErrorMessage name="description" component="div" />
                                    </Field>
                                    <Field label="Genre">
                                        <FormikField
                                            as={Input}
                                            name="genre"
                                            placeholder="Enter film genre"
                                        />
                                        <ErrorMessage name="genre" component="div" />
                                    </Field>
                                    <Field label="Director">
                                        <FormikField
                                            as={Input}
                                            name="director"
                                            placeholder="Enter director name"
                                        />
                                        <ErrorMessage name="director" component="div" />
                                    </Field>
                                    <Field label="Release Year">
                                        <FormikField
                                            as={Input}
                                            name="releaseYear"
                                            type="number"
                                            placeholder="Enter release year"
                                        />
                                        <ErrorMessage name="releaseYear" component="div" />
                                    </Field>
                                    <Field label="Rating">
                                        <FormikField
                                            as={Input}
                                            name="rating"
                                            type="number"
                                            placeholder="Enter film rating (1-10)"
                                        />
                                        <ErrorMessage name="rating" component="div" />
                                    </Field>
                                </Stack>
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
                                        type="submit"
                                        disabled={!isValid || !dirty}>
                                        Save
                                    </Button>
                                </DialogFooter>
                            </Form>
                        )}
                    </Formik>
                </DialogBody>
            </DialogContent>
        </DialogRoot>
    );
};

export default CreateUpdateFilm;