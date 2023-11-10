using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;
//using TicketEase.Common.Utilities;

namespace TicketEase.Application.ServicesImplementation
{
    public class BoardServices : IBoardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardServices> _logger;

        public BoardServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BoardServices> logger) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<BoardResponseDto>> AddBoardAsync(BoardRequestDto boardRequestDto)
        {
            ApiResponse<BoardResponseDto> response;

            try
            {   
                var existingBoard = _unitOfWork.BoardRepository.FindBoard(b => b.Name == boardRequestDto.Name).FirstOrDefault();
                if (existingBoard != null)
                {
                    response = new ApiResponse<BoardResponseDto>(false, 400, $"Board already exists.");
                    return response;
                }

                var board = _mapper.Map<Board>(boardRequestDto);
                _unitOfWork.BoardRepository.AddBoard(board);
                _unitOfWork.SaveChanges();
                
                var responseDto = _mapper.Map<BoardResponseDto>(board);
                response = new ApiResponse<BoardResponseDto>(true, $"Successfully added a board", 201, responseDto, new List<string>());
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a board");
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                response = new ApiResponse<BoardResponseDto>(true, "Error occurred while adding a board", 500, null, errorList);
                return response;
            }
        }

        //public async Task<ApiResponse<List<BoardResponseDto>>> GetAllBoardsAsync(int PerPage, int Page)
        //{
        //    try
        //    {
        //        var boards = _unitOfWork.BoardRepository.GetBoards();

        //        var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

        //        var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
        //            boardDtos,
        //            PerPage,
        //            Page,
        //            item => item.Name,
        //            item => item.Id 
        //        );

        //        return new ApiResponse<List<BoardResponseDto>>(true, 200, "Boards retrieved.", pagedBoardDtos.Data)
        //        {
        //            PerPage = pagedBoardDtos.PerPage,
        //            CurrentPage = pagedBoardDtos.CurrentPage,
        //            TotalPageCount = pagedBoardDtos.TotalPageCount,
        //            TotalCount = pagedBoardDtos.TotalCount
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while getting all boards.");
        //        return new ApiResponse<List<BoardResponseDto>>(false, 500, "Error occurred while getting all boards.", null, new List<string> { ex.Message });
        //    }
        //}


        //public async Task<ApiResponse<List<BoardResponseDto>>> GetAllBoardsAsync(int PerPage, int Page)
        //{
        //    try
        //    {
        //        var boards = _unitOfWork.BoardRepository.GetBoards();

        //        var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

        //        var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
        //            boardDtos,
        //            PerPage,
        //            Page,
        //            item => item.Name,
        //            item => item.Id
        //        );

        //        return new ApiResponse<List<BoardResponseDto>>(true, "Boards retrieved.", 200, pagedBoardDtos.Data)
        //        {
        //            PerPage = PerPage,
        //            CurrentPage = Page,
        //            TotalPageCount = pagedBoardDtos.TotalPageCount,
        //            TotalCount = pagedBoardDtos.TotalCount
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while getting all boards.");
        //        return new ApiResponse<List<BoardResponseDto>>(false, "Error occurred while getting all boards.", 500, null, new List<string> { ex.Message });
        //    }
        //}


        //public async Task<ApiResponse<GetBoardsDto>> GetAllBoardsAsync(int PerPage, int Page)
        //{
        //    try
        //    {
        //        var boards = _unitOfWork.BoardRepository.GetBoards();

        //        var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

        //        var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
        //            boardDtos,
        //            PerPage,
        //            Page,
        //            item => item.Name,
        //            item => item.Id
        //        );

        //        var getBoardsDto = new GetBoardsDto
        //        {
        //            Boards = (List<BoardResponseDto>)pagedBoardDtos.Data,
        //            PerPage = pagedBoardDtos.PerPage,
        //            CurrentPage = pagedBoardDtos.CurrentPage,
        //            TotalPageCount = pagedBoardDtos.TotalPageCount,
        //            TotalCount = pagedBoardDtos.TotalCount
        //        };

        //        return new ApiResponse<GetBoardsDto>(true, "Boards retrieved.", 200, getBoardsDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while getting all boards.");
        //        return new ApiResponse<GetBoardsDto>(false, "Error occurred while getting all boards.", 500, null, new List<string> { ex.Message });
        //    }
        //}
        public async Task<ApiResponse<GetBoardsDto>> GetAllBoardsAsync(int perPage, int page)
        {
            try
            {
                var boards = _unitOfWork.BoardRepository.GetBoards();

                var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

                var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
                    boardDtos,
                    perPage,
                    page,
                    item => item.Name,
                    item => item.Id
                );

                var getBoardsDto = new GetBoardsDto
                {
                    Boards = pagedBoardDtos.Data.ToList(),
                    PerPage = pagedBoardDtos.PerPage,
                    CurrentPage = pagedBoardDtos.CurrentPage,
                    TotalPageCount = pagedBoardDtos.TotalPageCount,
                    TotalCount = pagedBoardDtos.TotalCount
                };

                return new ApiResponse<GetBoardsDto>(true, "Boards retrieved.", 200, getBoardsDto, new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all boards.");
                return new ApiResponse<GetBoardsDto>(false, "Error occurred while getting all boards.", 500, null, new List<string> { ex.Message });
            }
        }
        //public async Task<ApiResponse<GetBoardsDto>> GetAllBoardsAsync(int PerPage, int Page)
        //{
        //    try
        //    {
        //        var boards = _unitOfWork.BoardRepository.GetBoards();

        //        var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

        //        var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
        //            boardDtos,
        //            PerPage,
        //            Page,
        //            item => item.Name,
        //            item => item.Id
        //        );

        //        var getBoardsDto = new GetBoardsDto
        //        {
        //            Boards = (List<BoardResponseDto>)pagedBoardDtos.Data,
        //            PerPage = pagedBoardDtos.PerPage,
        //            CurrentPage = pagedBoardDtos.CurrentPage,
        //            TotalPageCount = pagedBoardDtos.TotalPageCount,
        //            TotalCount = pagedBoardDtos.TotalCount
        //        };

        //        return new ApiResponse<GetBoardsDto>(true, "Boards retrieved.", 200, getBoardsDto, new List<string>());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while getting all boards.");
        //        return new ApiResponse<GetBoardsDto>(false, "Error occurred while getting all boards.", 500, null, new List<string> { ex.Message });
        //    }
        //}
        //public async Task<ApiResponse<GetBoardsDto>> GetAllBoardsAsync(int PerPage, int Page)
        //{
        //    try
        //    {
        //        var boards = _unitOfWork.BoardRepository.GetBoards();

        //        var boardDtos = _mapper.Map<List<BoardResponseDto>>(boards);

        //        var pagedBoardDtos = await Pagination<BoardResponseDto>.GetPager(
        //            boardDtos,
        //            PerPage,
        //            Page,
        //            item => item.Name,
        //            item => item.Id
        //        );

        //        // Check the type of pagedBoardDtos.Data before casting
        //        if (pagedBoardDtos.Data is List<BoardResponseDto> castedData)
        //        {
        //            var getBoardsDto = new GetBoardsDto
        //            {
        //                Boards = castedData,
        //                PerPage = pagedBoardDtos.PerPage,
        //                CurrentPage = pagedBoardDtos.CurrentPage,
        //                TotalPageCount = pagedBoardDtos.TotalPageCount,
        //                TotalCount = pagedBoardDtos.TotalCount
        //            };

        //            return new ApiResponse<GetBoardsDto>(true, "Boards retrieved.", 200, getBoardsDto, new List<string>());
        //        }
        //        else
        //        {
        //            // Handle the case where casting is not successful
        //            _logger.LogError("Failed to cast pagedBoardDtos.Data to List<BoardResponseDto>.");
        //            return new ApiResponse<GetBoardsDto>(false, "Error occurred while getting all boards.", 500, null, new List<string> { "Failed to cast pagedBoardDtos.Data to List<BoardResponseDto>." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while getting all boards.");
        //        return new ApiResponse<GetBoardsDto>(false, "Error occurred while getting all boards.", 500, null, new List<string> { ex.Message });
        //    }
        //}


        public async Task<ApiResponse<BoardResponseDto>> GetBoardByIdAsync(string id)
        {
            try
            {
                var board = _unitOfWork.BoardRepository.GetBoardById(id);

                if (board == null)
                {
                    return new ApiResponse<BoardResponseDto>(false, "Board not found.", 404, null, new List<string>());
                }

                var boardDto = _mapper.Map<BoardResponseDto>(board);

                return new ApiResponse<BoardResponseDto>(true, "Board found.", 200, boardDto, new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a board by ID.");
                return new ApiResponse<BoardResponseDto>(false, "Error occurred while getting a board by ID.", 500, null, new List<string> { ex.Message });
            }
        }

        public Task<ApiResponse<BoardResponseDto>> UpdateBoardAsync(string boardId, BoardRequestDto boardRequestDto)
        {
            //ApiResponse<BoardResponseDto> response;
            try
            {
                var existingBoard = _unitOfWork.BoardRepository.GetBoardById(boardId);
                if (existingBoard == null)
                {
                    return Task.FromResult(new ApiResponse<BoardResponseDto>(false, 400, $"Board not found."));
                    //return response;
                }

                var board = _mapper.Map(boardRequestDto, existingBoard);
                _unitOfWork.BoardRepository.UpdateBoard(existingBoard);
                _unitOfWork.SaveChanges();

                var responseDto = _mapper.Map<BoardResponseDto>(board);
                return Task.FromResult(new ApiResponse<BoardResponseDto>(true, $"Successfully added a board", 201, responseDto, new List<string>()));
                //return response;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while adding a board");
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return Task.FromResult(new ApiResponse<BoardResponseDto>(true, "Error occurred while adding a board", 500, null, errorList));
                //return response;

            }
        }
    }
}
