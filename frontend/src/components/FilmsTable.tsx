import {Button, Table} from "@chakra-ui/react"
import {Film} from "@/shared/apis/filmApi.ts";

export interface FilmsDataProps {
    films: Film[];
    onEdit: (film: Film) => void;
    onDelete: (filmId: string) => void;
}

const FilmsTable = ({films, onEdit, onDelete}: FilmsDataProps) => {
    return (
        <Table.Root size="lg">
            <Table.Header>
                <Table.Row>
                    <Table.ColumnHeader color="black">Title</Table.ColumnHeader>
                    <Table.ColumnHeader color="black">Genre</Table.ColumnHeader>
                    <Table.ColumnHeader color="black">Description</Table.ColumnHeader>
                    <Table.ColumnHeader color="black">Director</Table.ColumnHeader>
                    <Table.ColumnHeader color="black" textAlign="center">Release Year</Table.ColumnHeader>
                    <Table.ColumnHeader color="black" textAlign="center">Rating</Table.ColumnHeader>
                    <Table.ColumnHeader color="black" textAlign="center">Control</Table.ColumnHeader>
                    <Table.ColumnHeader color="black" textAlign="center">Control</Table.ColumnHeader>
                </Table.Row>
            </Table.Header>
            <Table.Body>
                {films.map((film) => (
                    <Table.Row key={film.id}>
                        <Table.Cell>{film.fullName.name}</Table.Cell>
                        <Table.Cell>{film.genre.title}</Table.Cell>
                        <Table.Cell>{film.fullName.description}</Table.Cell>
                        <Table.Cell>{film.director.fullName}</Table.Cell>
                        <Table.Cell textAlign="center">{film.release.year}</Table.Cell>
                        <Table.Cell textAlign="center">{film.rating.points}</Table.Cell>
                        <Table.Cell textAlign="center">
                            <Button 
                                bgColor="green" 
                                size="sm" 
                                color="white" 
                                padding={5} 
                                onClick={() => onEdit(film)}>
                                Edit
                            </Button>
                        </Table.Cell>
                        <Table.Cell textAlign="center">
                            <Button 
                                bgColor="red" 
                                size="sm" 
                                color="white" 
                                padding={5}
                                onClick={() => onDelete(film.id)}>
                                Delete
                            </Button>
                        </Table.Cell>
                    </Table.Row>
                ))}
            </Table.Body>
        </Table.Root>
    );
};

export default FilmsTable;