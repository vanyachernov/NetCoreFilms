import { Box, Input, Text } from "@chakra-ui/react";
import { Field } from "@/components/ui/field";
import { Radio, RadioGroup } from "@/components/ui/radio"

export interface SearchProps {
    onTitleSearch: (data: string) => void;
    onDirectorSearch: (data: string) => void;
    isRatingDescending: (data: boolean) => void;
}

const SearchField = (
    {
        onTitleSearch, 
        onDirectorSearch, 
        isRatingDescending
    }: SearchProps) => {
    return (
        <Box 
            display="flex"
            flexDirection="row"
            gap={10}>
            <Box
                display="flex"
                flexDirection="column"
                gap={5}>
                <Field label="Title">
                    <Input 
                        placeholder="Enter film title" 
                        variant="outline"
                        onChange={(e) => onTitleSearch(e.target.value)} />
                </Field>
                <Field label="Director">
                    <Input 
                        placeholder="Enter film director" 
                        variant="outline"
                        onChange={(e) => onDirectorSearch(e.target.value)} />
                </Field>
            </Box>
            <Box
                border="1px solid"
                borderColor="gray.300"
                padding={4}
                borderRadius="8px"
                display="flex"
                flexDirection="column"
                gap={2}>
                <Text 
                    fontWeight="bold"
                    marginBottom={2}>
                    By rating
                </Text>
                <RadioGroup
                    colorPalette="blue"
                    defaultValue="false"
                    display="flex"
                    flexDirection="column"
                    gap={3}
                    onValueChange={(e) => isRatingDescending(e.value)}>
                    <Radio value="false">Ascending</Radio>
                    <Radio value="true">Descending</Radio>
                </RadioGroup>
            </Box>
        </Box>
    );
};

export default SearchField;