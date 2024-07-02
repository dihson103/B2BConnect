export default function customFileName(file: File): File {
  const newImageRequest = URL.createObjectURL(file)
  const extension = file.name.substring(file.name.lastIndexOf('.'))

  const date = new Date()
  const year = date.getFullYear().toString()
  const month = (date.getMonth() + 1).toString().padStart(2, '0')
  const day = date.getDate().toString().padStart(2, '0')
  const hour = date.getHours().toString().padStart(2, '0')
  const minute = date.getMinutes().toString().padStart(2, '0')
  const second = date.getSeconds().toString().padStart(2, '0')

  const changedFileName = `images-${year}${month}${day}${hour}${minute}${second}${extension}`
  const newFile = new File([file], changedFileName, { type: file.type })
  return newFile
}
