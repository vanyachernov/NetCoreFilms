import { Button } from "@/components/ui/button"
import {
    DialogActionTrigger,
    DialogBody,
    DialogCloseTrigger,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogRoot,
    DialogTitle,
} from "@/components/ui/dialog"

interface Props {
    filmId: string;
    isModalIs: boolean;
    handleCancel: () => void;
    handleDelete: (id: string) => void;
}

const DeleteFilmAlert = ({
    filmId,
    isModalIs,
    handleCancel,
    handleDelete} : Props) => {
    return (
        <DialogRoot 
            role="alertdialog"
            open={isModalIs}
            closeOnEscape={true}
            closeOnInteractOutside={true}>
            <DialogContent bg="white" color="black">
                <DialogHeader>
                    <DialogTitle>Are you sure?</DialogTitle>
                </DialogHeader>
                <DialogBody>
                    <p>
                        This action cannot be undone. This will permanently delete this film.
                    </p>
                </DialogBody>
                <DialogFooter gap={5}>
                    <DialogActionTrigger asChild>
                        <Button variant="outline" onClick={handleCancel}>Cancel</Button>
                    </DialogActionTrigger>
                    <Button colorPalette="red"
                            bgColor="red"
                            padding={5}
                            color="white"
                            onClick={() => handleDelete(filmId)}>Delete</Button>
                </DialogFooter>
                <DialogCloseTrigger />
            </DialogContent>
        </DialogRoot>
    );
};

export default DeleteFilmAlert;