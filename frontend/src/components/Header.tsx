import {Box, Button, Text} from "@chakra-ui/react";

interface HeaderProps {
    onNewFilm: () => void;
}

const Header = ({onNewFilm} : HeaderProps) => {
    return (
        <Box height={20} 
             bgColor="green" 
             display="flex"
             justifyContent="space-between"
             alignItems="center"
             paddingLeft={10}
             fontSize={18}
             fontWeight="bold">
            <Text 
                bgColor="green"
                fontSize={25}
                color="white">
                Films
            </Text>
            
            <Button
                bgColor="gray"
                padding={5}
                marginRight={10}
                onClick={onNewFilm}
                color="white">
                New Film
            </Button>
        </Box>
    );
};

export default Header;