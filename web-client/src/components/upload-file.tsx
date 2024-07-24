'use client'

import FileCard from '@/components/file-uploader'
import { Input } from '@/components/ui/input'
import { ScrollArea } from '@/components/ui/scroll-area'
import { Dispatch, SetStateAction } from 'react'

type Props = {
  files: (File | string)[]
  setFiles: Dispatch<SetStateAction<(File | string)[]>>
  mainImageIndex: number | null
  setMainImageIndex: Dispatch<SetStateAction<number | null>>
}

export default function UploadFiles({ files, setFiles, mainImageIndex, setMainImageIndex }: Props) {
  const handleUploadPhoto = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newFiles = e.target.files ? Array.from(e.target.files) : []
    setFiles((prevFiles) => [...prevFiles, ...newFiles])
    if (mainImageIndex == null) setMainImageIndex(0)
  }

  const onRemove = (index: number) => {
    setFiles((prevFiles) => prevFiles.filter((_, i) => i !== index))
    if (mainImageIndex === index) {
      if (files.length > 1) {
        setMainImageIndex(0)
      } else {
        setMainImageIndex(null)
      }
    } else if (mainImageIndex !== null && index < mainImageIndex) {
      setMainImageIndex(mainImageIndex - 1)
    }
  }

  const handleMainImageChange = (index: number) => {
    setMainImageIndex(index)
  }

  return (
    <>
      <Input id='picture' type='file' multiple onChange={handleUploadPhoto} />
      {files.length > 0 && (
        <ScrollArea className='h-fit w-full px-3'>
          <div className='max-h-48 space-y-4'>
            {files.map((file, index) => (
              <FileCard
                key={index}
                file={typeof file === 'string' ? undefined : file}
                onRemove={() => onRemove(index)}
                isMain={index === mainImageIndex}
                onMainImageChange={() => handleMainImageChange(index)}
                fileName={typeof file === 'string' ? file : file?.name}
              />
            ))}
          </div>
        </ScrollArea>
      )}
    </>
  )
}
